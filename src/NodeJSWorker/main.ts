import {ZBClient} from 'zeebe-node'
import {v4 as uuid} from 'uuid'

const zbc = new ZBClient()

interface ProcessPayload {
    person_uuid: string // Message correlation key 
    isHealthy?: boolean // Set via user task
    isZombie?: boolean // Set via message correlation
    quarantine_duration?: string // Set via Business Rules task
    recovery_certificate_uuid?: string // Set in service task
}

zbc.createWorker<ProcessPayload, {}, Partial<ProcessPayload>>({
    taskType: 'determine_quarantine_period',
    taskHandler: job => {
        // Do Business Rules here
        return job.complete({
            quarantine_duration: 'PT2D'
        })
    }
})

zbc.createWorker<ProcessPayload, {}, Partial<ProcessPayload>>({
    taskType: 'notify_person_to_quarantine',
    taskHandler: job => {
        console.log(`Retrieving contact details for person ${job.variables.person_uuid} from external database...`)
        console.log(`Sending notification to person ${job.variables.person_uuid} to quarantine...`)
        return job.complete()
    }
})

zbc.createWorker<ProcessPayload, {}, Partial<ProcessPayload>>({
    taskType: 'generate_certificate_of_recovery',
    taskHandler: job => {
        const recovery_certificate_uuid = uuid()
        console.log(`Generating certificate of recovery for person ${job.variables.person_uuid}...`)
        console.log(`Generated certificate ID: ${recovery_certificate_uuid}`)
        console.log(`Storing Recovery Certificate in external database...`)
        return job.complete({
            recovery_certificate_uuid
        })
    }
})

zbc.createWorker<ProcessPayload, {}, Partial<ProcessPayload>>({
    taskType: 'send_certificate_of_recovery',
    taskHandler: job => {
        console.log(`Retrieving Recovery Certificate ${job.variables.recovery_certificate_uuid} from external database...`)
        console.log(`Retrieving contact details for person ${job.variables.person_uuid} from external database...`)
        console.log(`Sending Recovery Certificate to person ${job.variables.person_uuid}...`)
        return job.complete()
    }
})