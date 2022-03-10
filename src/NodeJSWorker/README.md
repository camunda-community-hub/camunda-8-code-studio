# Node JS Worker Code

## Prerequisites

To run the Node JS worker code, you need [Node JS](https://nodejs.org/en/) version >= 16.6.1, >=14.17.5, or >=12.22.5.

You also need the TypeScript compiler installed. If you don't already have it, once you have Node JS installed, you can install the TypeScript compiler with:

```bash
npm i -g typescript
```

## Installation

After cloning the source code, in this directory, install the dependencies with the following command: 

```bash
npm i
```

This will install the project dependencies from NPM.

## Configuration

The code uses the [Zero-conf constructor](https://github.com/camunda-community-hub/zeebe-client-node-js#zero-conf), so you will need to provide the credentials for your Camunda Cloud cluster via the environment.

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