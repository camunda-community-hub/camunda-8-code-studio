# The HZV Quarantine Process

![](img/HZV_Process.png)

## Start Event

The process is started by a [Message Start Event](https://docs.camunda.io/docs/components/modeler/bpmn/message-events/#message-start-events).

Semantically, this indicates that a positive HZV test notification has been received by the system. Technically, this is accomplished by calling the `PublishMessage` gRPC API, and specifying the message name `Positive_HZV_Test`.

You can verify that this is the message name by opening the process model [bpmn/hzvprocess.bpmn](bpmn/hzvprocess.bpmn) using the [Camunda Modeler](https://camunda.com/download/modeler/), or [importing it](https://docs.camunda.io/docs/components/modeler/web-modeler/import-diagram/) to the [Camunda Cloud Web Modeler](https://modeler.cloud.camunda.io/).

## Business Rule Task

The next task, "_Determine length of quarantine_", is a [Business Rule Task](https://docs.camunda.io/docs/components/modeler/bpmn/business-rule-tasks/). Semantically, this is a task that uses DMN (Decision Model Notation) to determine how long the quarantine period will be. Technically, this is a service task that requires a worker to execute. All tasks in Camunda Cloud use the external task worker pattern. You can use the [Zeebe DMN Worker](https://github.com/camunda-community-hub/zeebe-dmn-worker) to service jobs for this task.

## Message Send Task

The next task, "_Notify person to quarantine_", is a [Message Send Task](https://docs.camunda.io/docs/components/modeler/bpmn/send-tasks/). Semantically, this is a task that sends a message to an external system. Technically, it is a service task - as are all tasks in Camunda Cloud. The Zeebe engine uses the external task pattern for all tasks.

## In Quarantine Sub-process

The quarantine period - "_In Quarantine_" - is modeled as an [Embedded Sub-process](https://docs.camunda.io/docs/components/modeler/bpmn/embedded-subprocesses/). This allows the quarantine to be interrupted by boundary events.

## User Task

The first task in the quarantine sub-process - "_Check on infected person_" - is a [User Task](https://docs.camunda.io/docs/components/modeler/bpmn/user-tasks/).

A User Task, semantically, is a task that is completed by a human. Technically, it is a service task of type `io.camunda.zeebe:userTask`. In Camunda Cloud, jobs of this type are serviced by [Tasklist](https://docs.camunda.io/docs/components/tasklist/introduction/). The Tasklist application provides an interface for humans to complete jobs. You specify a form to use for the job in the task definition in the process model, in the `Form JSON Configuration` property.

## Exclusive Gateway

The next element - "_Person is Zombie?_" - is an [exclusive gateway](https://docs.camunda.io/docs/components/modeler/bpmn/exclusive-gateways/). Based on the evaluation of the condition, the process will take one of the branches out of the gateway. In this case, if the person has turned into a zombie, the sub-process will end. If the person is not a zombie, the process will go to the timer event.

## Timer Event

The "_Wait for two days_" [timer event](https://docs.camunda.io/docs/components/modeler/bpmn/timer-events/) has the timer duration set to PT2D, which means that the timer will fire 48 hours (two days) after the process token enters the timer event.

While the process token is waiting at this timer event, the entire sub-process can be interrupted by either of the two boundary interrupting events on the sub-process.

## Interrupting boundary timer event

The "_Quarantine finishes officially_" timer event is an [interrupting boundary event](https://docs.camunda.io/docs/components/modeler/bpmn/timer-events/#timer-boundary-events). If this timer fires at any point while the token is inside this sub-process, the sub-process will terminate, and the token will flow out through this event.

The duration of the timer is dynamically set from the Business Rule Task at the outset of the process. A timer duration can be a static value, or it can be a FEEL expression. This allows us to set the timer duration from a payload variable.

## Service Task

The "_Generate certificate of recovery_" service task is semantically and technically a [service task](https://docs.camunda.io/docs/components/modeler/bpmn/service-tasks/). All tasks in Camunda Cloud are technically service tasks, as the Zeebe engine uses the external task worker pattern for everything. Semantically, this is a service task where a worker creates a certificate of recovery for the patient, who has either recovered or spent sufficient time in quarantine.

## Message Throw Event

The "_Send certificate of recovery_" [message throw event](https://docs.camunda.io/docs/components/modeler/bpmn/message-events/#message-throw-events) is semantically the emission of a message to an external system. Technically, it is a service task that must be serviced by a job worker. The job worker is responsible for emitting the message to the external system. 
