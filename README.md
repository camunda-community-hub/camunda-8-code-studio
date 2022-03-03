# Camunda-Cloud Code Studio
<img src="https://img.shields.io/badge/Version-Under%20Construction-red">
<img src="https://img.shields.io/badge/Tutorial%20Reference%20Project-Tutorials%20for%20getting%20started%20with%20Camunda-%2338A3E1)">
<img src="https://img.shields.io/badge/Camunda%20DevRel%20Project-Created%20by%20the%20Camunda%20Developer%20Relations%20team-0Ba7B9">

During the pandemic citizens of different countries noticed their governments using their resources and processes inefficiently. 
In this workshop we are going to change this! How can we help the health department to get their processes straight? 
Our focus will be centered around the quarantine. Of course, we will be doing this using the magic of Camunda Cloud and BPMN.  

Welcome to Camunda's Cloud Code studio, y'all! These exercises and notes have been designed for an actual workshop. 
They act as a resource for the moderator, but you can also follow the exercises here without attending an actual event -
just imagine that you hear your favorite Developer Advocate talking to you. The readme contains detailed instruction on 
how to complete the exercises. In the other folders, you'll find the model solutions as well as the full code solutions.
The presentation from the workshop is provided as well.

**To structure this course we are going to split it into 6 exercises:**  
1. What kind of process can you imagine to make your government more efficient in processing COVID-19 cases? - Design it! 
2. Having aligned on one process, we now need to automate things. But how? - Get familiar with Camunda Cloud and discover features! 
3. Design / Implement a first draft of the process with User Tasks and Forms!
4. Automating it - Choosing a Camunda Cloud Client and implementing a Service Task!
5. Adding more complexity to your executable process! 
6. Analysis time - Is our process better than the government one? Generate awesome dashboards in Optimize!

# Table of Contents
* 🚀 [Getting Started](#getting-started)
* 🎓 [Excercises](#excercises)
  * [Excercise 2: Get familiar with Camunda Cloud and discover features](#excercise-2-get-familiar-with-camunda-cloud-and-discover-features)


# 🚀Getting Started
This section describes the prerequisites for this course. In order to participate, you need to set up a development environment 
of your choice. We are going to support you either in Node.js, Spring Boot, DotNet or Python. Please make sure you have 
the supported version and an IDE of your choice installed. 

| Environment   | Supported Version|
| ------------- | ---------------- |
| Node.js       | >= 16.6.1, >=14.17.5, or >=12.22.5|
| .NET        | .NET standard 2.0 or higher, .NET core 2.1 or higher or .NET framework 4.7.1 or higher|
| Spring Boot   | Java 8 or higher (Java 11 or higher recommended)    |
| Python        | Python 3.6 or higher   |

It is also recommended to sign up for the Camunda Cloud trial in front of the workshop. This trial will last for 30 days, 
so make sure it is still active when the workshop is taking place. You can sign up [here](https://accounts.cloud.camunda.io/signup).

# 🎓Excercises
## Excercise 2: Get familiar with Camunda Cloud and discover features 
After having modelled and aligned on our process diagram we are now going to discover the capabilities of Camunda Cloud.

Camunda Cloud is powered by Zeebe, a new class of BPMN workflow engine that delivers true horizontal scalability and enables 
high-performance use cases that were once beyond the realm of workflow automation. It is architected for the cloud 
from the ground up and is ideally for cloud application use cases such as microservices-based applications and integrates
seamlessly with best-in-class cloud components.

The [Cloud Console](https://console.cloud.camunda.io/) acts as entry point to Camunda Cloud. After having logged in or signed 
up for a trial you will be redirected to the landing page. 
![Cloud Console Landing Page](./img/CloudConsole_LandingPage.png)
From this page onwards you can do various things: 
* It's the home of the **Organization** you are part of. So you can manage your team members accordingly. 
* Open up the **Web Modeler** to create some awesome collaborative BPMN models.
* Get to manage your **Clusters** i.e. to boot up a new one or to access an existing one. 
* And of course you have quick access to various **knowledge resources**.

Since you have already modeled a process in the previous task lets take a look at our Clusters for now. 
If you have not created one yet make sure to create a new one. Make sure to set a suitable name. 
![Cluster Creation](./img/CloudClusterCreation.png) 

After some time the cluster has been created and you should be able to see how the various applications are switching to healthy. 
As soon as this has happened you are ready to use them by clicking the *launch* button. For now we do not need to create a 
Client as the orange notification suggests. That will be part of the next exercise. 
![Cluster View](./img/ClusterView.png)
Now it is your task to familiarize yourself with these tools. Let's quickly sum up what they are actually doing. Keep in mind
that we are going to revisit them in more depth during the next exercise. 
* **Zeebe**: A new class of BPMN workflow engine that delivers true horizontal scalability and enables high-performance use cases.
* **Operate**: A tool designed for teams to manage, monitor, troubleshoot running workflow instances.
* **Tasklist**: It is an out-of-the-box web application that’s tightly integrated with Camunda’s process orchestration capabilities. 
Simply model a business process and deploy it to the Workflow Engine; when a user needs to work on a task, they’ll see it appear in Tasklist.
* **Optimize**: Create business-friendly reports and dashboards as well as alerts that help you identify process bottlenecks 
and improve your overall end-to-end process.

Enjoy experimenting around! 


## Exercise 3: Design a process with User Tasks and Forms
We have created a process in our Camunda Cloud environment during [exercise 1](#excercise-1) already. In this section we want to
deploy this process after having added Camunda Forms to a User Task. 

In the animated image below you can see how to **change the type of activity**. After having done so we can change some User Task
specific properties such as the assignee. In addition, you can find an empty property for a form in there. 
![Create a User Task](./img/CreateUserTask.gif)

After accomplishing that we can now focus on **creating a Form** by using an intuitive form builder. You can select to create a new
one in your Modeler project. The form builder allows you to drag and drop common elements used for forms onto a canvas. Of course, you can 
set certain properties right in there. After finished configuring your form you can also take a log on the JSON based representation.
![Create a Form](./img/CreateForm.gif)

> Now feel free to create a meaningful form for your User-Task

After having accomplished that we need to figure out how to **attach to a Form to a User Task**. Please go back to your BPMN
Model and select your task for this purpose. Now you have two possibilities:
* Either you can click on the "blue context menu" to choose and import an available Form
* Or you can copy the JSON shown before and add it manually to the "Form JSON configuration"
![Attach a Form](./img/AttachForm.gif)

Now we are good to deploy our process model to the engine! Click the "Deploy Diagram" Button to see if there aren't any syntactic 
mistakes in your model. Afterwards you can start a process instance by clicking on the related button. You should now be able to observe
the progress in Operate and work on the User Task in Tasklist. 
In the end you will be able to see that the process has ended successfully. 
![Start a Porcess Instance](./img/StartProcessInstance.gif)

Congratulations 🎉! You have managed to start and execute your first process instance containing only a User Task! Now you have
earned yourself a little break ☕️🥐.