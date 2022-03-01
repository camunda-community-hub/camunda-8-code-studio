from pyzeebe import ZeebeTaskRouter

router = ZeebeTaskRouter()

@router.task(task_type="notification-worker")
async def my_task():
    return {"output": f"Hello world!"}