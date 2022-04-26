import asyncio
import os
import re

from pyzeebe import ZeebeWorker, Job, create_insecure_channel, create_camunda_cloud_channel

# IMPORTANT:
# 
# Create your API credentials in camunda cloud first and download the credentials.
# The download file is a txt-file that holds export statements for setting the following environment variables:
#
# export ZEEBE_ADDRESS='xxxxxxxx.bru-2.zeebe.camunda.io:443'
# export ZEEBE_CLIENT_ID='yyyyyyyyy'
# export ZEEBE_CLIENT_SECRET='zzzzzzzzzzz'
# export ZEEBE_AUTHORIZATION_SERVER_URL='https://login.cloud.camunda.io/oauth/token'

zeebe_client_id = os.environ.get('ZEEBE_CLIENT_ID')
assert zeebe_client_id

zeebe_client_secret = os.environ.get('ZEEBE_CLIENT_SECRET')
assert zeebe_client_secret

zeebe_address = os.environ.get('ZEEBE_ADDRESS')
assert zeebe_address

match = re.search(r'(?P<cluster_id>[^\.]*)\..*',zeebe_address)
camundacloud_cluster_id = match.group('cluster_id')
assert camundacloud_cluster_id

channel = create_camunda_cloud_channel(client_id=zeebe_client_id, client_secret=zeebe_client_secret, cluster_id=camundacloud_cluster_id)
worker = ZeebeWorker(channel) # Create a zeebe worker


async def on_error(exception: Exception, job: Job):
    """
    on_error will be called when the task fails
    """
    print(exception)
    await job.set_error_status(f"Failed to handle job {job}. Error: {str(exception)}")


@worker.task(task_type="notify_person_to_quarantine", exception_handler=on_error)
def notify_person_to_quarantine(quarantine_duration: str) -> dict:
    print(f"You're sick! Stay in for {quarantine_duration}!")
    return {"output": f"You're sick! Stay in for {quarantine_duration}!"}


@worker.task(task_type="generate_certificate_of_recovery", exception_handler=on_error)
async def generate_certificate_of_recovery(person_uuid: str) -> dict: # Tasks can also be async
    print(f"Dear {person_uuid}, you are fully recovered!")
    return {"certificate": f"Dear {person_uuid}, you are fully recovered!"}

@worker.task(task_type="send_certificate_of_recovery", exception_handler=on_error)
async def generate_certificate_of_recovery(certificate: str) -> dict: # Tasks can also be async
    print(f"New certificate: {certificate}")

loop = asyncio.get_event_loop()
loop.run_until_complete(worker.work())
