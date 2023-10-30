package zeebe.camunda.worker;

import io.camunda.zeebe.client.ZeebeClient;
import io.camunda.zeebe.client.api.response.ActivatedJob;
import io.camunda.zeebe.spring.client.annotation.JobWorker;
import io.camunda.zeebe.spring.client.annotation.Variable;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import java.util.UUID;
import java.util.logging.ConsoleHandler;
import java.util.logging.Level;
import java.util.logging.Logger;


@SpringBootApplication
public class WorkerApplication {
	static Logger logger = Logger.getLogger(Logger.GLOBAL_LOGGER_NAME);
	static ConsoleHandler handler = new ConsoleHandler();
	@Autowired
	private ZeebeClient client;

	public static void main(String[] args) {
		SpringApplication.run(WorkerApplication.class, args);
		logger.setLevel(Level.ALL);
		handler.setLevel(Level.ALL);
		logger.addHandler(handler);
	}

	@JobWorker(type = "notify_person_to_quarantine")
	public void notifyPersonToQuarantine(final ActivatedJob job, @Variable String person_uuid) {
		logger.info("Retrieving contact details for person " + person_uuid + " from external database...");
		logger.info("Sending notification to person " + person_uuid + " to quarantine...");
	}

	@JobWorker(type = "generate_certificate_of_recovery")
	public void generateCertificateOfRecovery(final ActivatedJob job, @Variable String person_uuid) {
		UUID recovery_certificate_uuid = UUID.randomUUID();
		logger.info("Generating certificate of recovery for person " + person_uuid +"...");
		logger.info("Generated certificate ID: " + recovery_certificate_uuid);
		logger.info("Storing Recovery Certificate in external database...");

		client.newCompleteCommand(job.getKey())
				.variables("{\"recovery_certificate_uuid\": \"" + recovery_certificate_uuid + "\"}")
				.send()
				.exceptionally( throwable -> { throw new RuntimeException("Could not complete job " + job, throwable); });
	}

	@JobWorker(type = "send_certificate_of_recovery")
	public void sendCertificateOfRecovery(final ActivatedJob job, @Variable String person_uuid, @Variable String recovery_certificate_uuid) {
		logger.info("Retrieving Recovery Certificate " + recovery_certificate_uuid + " from external database...");
		logger.info("Retrieving contact details for person " + person_uuid + "from external database...");
		logger.info("Sending Recovery Certificate to person " + person_uuid + ". Enjoy that ice-cream!");
	}
}
