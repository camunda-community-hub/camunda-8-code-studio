import asyncio
from pyzeebe import ZeebeWorker, create_camunda_cloud_channel
from Task import router

async def main():
    channel = create_camunda_cloud_channel("client_id", "client_secret", "cluster_id")
    worker = ZeebeWorker(channel)
    worker.include_router(router)
    await worker.work()

asyncio.run(main())