# Node.js Worker Code

We provide an example implementation, and also instructions on how to create a worker project from scratch. You can clone and run the prebuilt example, or write your own, and refer to the example code for reference when needed.

For instructions on using the prebuilt example, refer to the later section "Pre-built solution".

Note the prerequisites below, which apply to any solution using the Zeebe Node.js client.

## Prerequisites

To run the Node JS worker code, you need [Node JS](https://nodejs.org/en/) version >= 16.6.1, >=14.17.5, or >=12.22.5.

## Bootstrap a project

* Make sure that you have TypeScript installed: 

```bash
npm i -g typescript
```

* Create a new TypeScript project:

```bash
npm init -y
tsc --init
```

* Install the Zeebe Node client as a dependency: 

```bash
npm i zeebe-node
```

## Build the workers

See the README section on [writing Job Workers](https://github.com/camunda-community-hub/zeebe-client-node-js/blob/master/README.md#the-zbworker-job-worker).

# Pre-built solution

We have a pre-built solution that you can examine and run.

## Installation

After cloning the source code, in this directory, install the dependencies with the following command:

```bash
npm i
```

This will install the project dependencies from NPM.

## Configuration

The code uses the [Zero-conf constructor](https://github.com/camunda-community-hub/zeebe-client-node-js#zero-conf) to get the connection configuration from the environment, so you need to provide the credentials for connecting to your Camunda Platform 8 cluster.

[Create an API client](https://docs.camunda.io/docs/components/console/manage-clusters/manage-api-clients/#create-a-client) and download the credentials file for your API Client from the Cloud Console.

Rename the file `env.example` to `.env` and replace the contents with the credentials from the credentials file.

## Run

To run the workers, run:

```bash
npm start
```

This will build the TypeScript code, then execute it.

### JSON Payload to start the process
```shell
  {
    "person_uuid": "Carl",
    "employment_category": "critical infrastructure",
    "age": 25
  }
```
