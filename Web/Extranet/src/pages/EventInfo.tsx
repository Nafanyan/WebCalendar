import { FunctionComponent, useState, useEffect } from "react"
import { Button, Modal } from "react-bootstrap"
import { useDispatch } from "react-redux"
import "../css/month-calendar.css"
import { TimeToString, TimeToStringRequest } from "../custom-functions/TimeToString"
import { useTypedSelector } from "../hooks/useTypeSelector"
import { IEvent } from "../models/query/IEvent"
import { EventService } from "../services/EventService"

export interface EventInfoProps {
    startEvent: Date,
    endEvent: Date
}

export const EventInfo: FunctionComponent<EventInfoProps> = ({ startEvent, endEvent }) => {
    const { userId, nextRendering } = useTypedSelector(status => status.currentDay)
    const dispatch = useDispatch()
    const [show, setShow] = useState(false)
    const [event, setEvent] = useState<IEvent>(
        {
            id: userId,
            name: "",
            description: "",
            startEvent: new Date(startEvent),
            endEvent: new Date(endEvent)
        }
    );


    useEffect(() => {
        const fetchEvent = async () => {
            const service: EventService = new EventService();
            let startEventStr: string = TimeToStringRequest(new Date(startEvent))
            let endEventStr: string = TimeToStringRequest(new Date(endEvent));
            let fetchedEvent: IEvent = await service.get(userId, startEventStr, endEventStr);
            setEvent(fetchedEvent);
            console.log(event);
        }
        if (show)
        {
            fetchEvent(); 
        }
    },[show])

    const handleClose = () => {
        setShow(false)
    }
    const handleShow = async () => {
        setShow(true)
    }

    return (
        <>
            <Button variant="light" onClick={handleShow}>
                {TimeToString(startEvent) + " - " + TimeToString(endEvent)}
            </Button>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Данные события</Modal.Title>
                </Modal.Header>
                <div>{event.name}</div>
                <div>{event.description}</div>
                {/* <div>{TimeToString(event.startEvent)}</div>
                <div>{TimeToString(event.endEvent)}</div> */}

                <Modal.Body>


                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Выйти
                    </Button>
                    <Button variant="primary">
                        Удалить событие
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    )
}

export default EventInfo

