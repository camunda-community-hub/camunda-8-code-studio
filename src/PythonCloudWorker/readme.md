## Python Worker 
This folder holds all the code for the Python version of the Camunda 8 Worker. As client implementation we have used the pyzeebe dependency. You can find the GitHub repository right [here](https://github.com/camunda-community-hub/pyzeebe).

## Preparation
Install Python: https://python.org

### Install dependencies
The `requirements.txt` contains all dependencies necessary:
```shell
> pip install -r src/PythonWorker/requirements.txt
```

### Export zeebe client credentials
You need to configure properties for Camunda Platform 8 in your environment. All necessary information can be retrieved from the Camunda 8 console (Cluster API view) right away. Remember to first of all create a client credential in order to make this possible. You then need to use the `Download credentials` function. The resulting txt-file contains a few export statements like this:

```shell
export ZEEBE_ADDRESS='xxxxxxxx.bru-2.zeebe.camunda.io:443'
export ZEEBE_CLIENT_ID='yyyyyyyyy'
export ZEEBE_CLIENT_SECRET='zzzzzzzzzzz'
export ZEEBE_AUTHORIZATION_SERVER_URL='https://login.cloud.camunda.io/oauth/token'
```

*Execute these exports on your console*, for getting your credentials set in your local environment. `PythonWorker.py` will use those credentials from your environment automatically.

## Usage

### JSON Payload to start the process
Open the process model in Camunda Cloud an start a new process instance with following payload:
```json
  {
    "person_uuid": "Carl",
    "employment_category": "critical infrastructure",
    "age": 25
  }
```

### Run PythonWorker
Execute on console:

```shell
> python src/PythonWorker/PythonWorker.py
```

You get an output on the console for each task:
```shell
> python src/PythonWorker/PythonWorker.py
You're sick! Stay in for PT20S!
Dear Carl, you are fully recovered!
New certificate: Dear Carl, you are fully recovered!
```

## Advice:
Reduce the duration for staying in quarantine from days to seconds. Otherwise you need to wait actual days for a process instance to finish.