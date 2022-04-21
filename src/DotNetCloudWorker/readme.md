## .NET Camunda Platform 8 Worker
This folder holds all the code for the .NET Standard version of the Camunda 8 Worker. As client implementation we have used the zb-client which is available on nuget right [here](https://www.nuget.org/packages/zb-client/). The corresponding GitHub repository can be found right [here](https://github.com/camunda-community-hub/zeebe-client-csharp).

After having imported the nuget dependency you can start right away by taking a look into the main method of this solution. 

### JSON Payload to start the process
```shell
  {
    "person_uuid": "Carl",
    "employment_category": "critical infrastructure",
    "age": 25
  }
```