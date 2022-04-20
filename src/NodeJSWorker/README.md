# Node JS Worker Code

## Prerequisites

To run the Node JS worker code, you need [Node JS](https://nodejs.org/en/) version >= 16.6.1, >=14.17.5, or >=12.22.5.

## Bootstrap a project

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



# Pre-built solution

We have a pre-built solution that you can examine and run.

## Installation

After cloning the source code, in this directory, install the dependencies with the following command:

```bash
npm i
```

This will install the project dependencies from NPM.

## Configuration

The code uses the [Zero-conf constructor](https://github.com/camunda-community-hub/zeebe-client-node-js#zero-conf), so you will need to provide the credentials for your Camunda Platform 8 cluster via the environment.

The following three environment variables need to be set:

```bash
ZEEBE_ADDRESS
ZEEBE_CLIENT_SECRET
ZEEBE_CLIENT_ID
```

## Run

To run the workers, run:

```bash
npm start
```

This will build the TypeScript code, then execute it.

