package zeebe.camunda.worker;

import io.camunda.zeebe.client.ZeebeClient;
import io.camunda.zeebe.client.api.response.ActivatedJob;
import io.camunda.zeebe.client.api.worker.JobClient;
import io.camunda.zeebe.client.impl.oauth.OAuthCredentialsProvider;
import io.camunda.zeebe.client.impl.oauth.OAuthCredentialsProviderBuilder;
import io.camunda.zeebe.spring.client.EnableZeebeClient;
import io.camunda.zeebe.spring.client.annotation.ZeebeWorker;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.RestController;


@SpringBootApplication
@EnableZeebeClient
@RestController
public class WorkerApplication {
	public static void main(String[] args) {
		SpringApplication.run(WorkerApplication.class, args);
	}

	@ZeebeWorker(type = "recipe-worker")
	public void handleJobFoo(final JobClient client, final ActivatedJob job) {

		client.newCompleteCommand(job.getKey())
				.variables("...")
				.send()
				.exceptionally( throwable -> { throw new RuntimeException("Could not complete job " + job, throwable); });
	}

}
