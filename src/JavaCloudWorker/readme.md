## Java Cloud Worker 
This folder holds all the code for the Java Spring Boot version of the Camunda Cloud Worker. As client implementation we have used the spring-zeebe dependency. You can find the GitHub repository right [here](https://github.com/camunda-community-hub/spring-zeebe).

In case you want to add this into your project as well just use this dependency in your pom file:
```shell
<dependency>
  <groupId>io.camunda</groupId>
  <artifactId>spring-zeebe-starter</artifactId>
  <version>${CURRENT_VERSION}</version>
</dependency>
```

You need to configure properties for CamundaCloud in your resources folder under "application.properties". All necessary information can be retrieved from the cloud console (Cluster API view) right away. Remember to first of all create a client credential in order to make this possible. 