import { FunctionComponent, useState, useEffect } from "react"
import { Button, Form, Modal } from "react-bootstrap"
import { useDispatch } from "react-redux"
import "../css/month-calendar.css"
import { TimeToString, TimeToStringCommand, TimeToStringRequest } from "../custom-functions/TimeToString"
import { useTypedSelector } from "../hooks/useTypeSelector"
import { IEvent } from "../models/IEvent"
import { EventService } from "../services/EventService"
import { IEventQueryResult } from "../models/query/IEventQuery"
import { IValidationResult } from "../models/IValidationResult"
import { CurrentDayActionType } from "../models/type/currentDay"

export interface EventInfoProps {
    startEvent: Date,
    endEvent: Date
}

export const EventInfo: FunctionComponent<EventInfoProps> = ({ startEvent, endEvent }) => {
    const { userId, nextRendering } = useTypedSelector(status => status.currentDay)
    const dispatch = useDispatch()

    const [show, setShow] = useState(false)

    const [event, setEvent] = useState<IEventQueryResult>(
        {
            objResult: {
                userId: userId,
                name: " ",
                description: " ",
                startEvent: startEvent,
                endEvent: endEvent
            },
            validationResult: {
                IsFail: false,
                Error: null
            }
        }
    );

    const handleClose = () => {
        setShow(false)
    }

    const handleShow = async () => {
        setShow(true)
        const service: EventService = new EventService();
        let startEventStr: string = TimeToStringRequest(new Date(startEvent))
        let endEventStr: string = TimeToStringRequest(new Date(endEvent));
        setEvent(await service.get(userId, startEventStr, endEventStr));
    }

    const deleteEvent = async () => {
        const service: EventService = new EventService()
        let startEventStr: string = TimeToStringCommand(new Date(startEvent), TimeToString(startEvent))
        let endEventStr: string = TimeToStringCommand(new Date(endEvent), TimeToString(endEvent))

        let result: IValidationResult = await service.delete(userId, {
            StartEvent: startEventStr,
            EndEvent: endEventStr
        })

        dispatch({ type: CurrentDayActionType.FORCED_DEPENDENCY_RENDER, nextRendering: !nextRendering })
        handleClose()
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
                {event.objResult.name != " " &&
                    <>
                        <Modal.Body>
                            <Form>
                                <Form.Group
                                    className="name-event-info"
                                    controlId="name-event-info-Textarea"
                                >
                                    <Form.Label>Название события:</Form.Label>
                                    <Form.Control as="textarea" rows={1}
                                        disabled
                                        readOnly
                                    >
                                        {event.objResult.name}
                                    </Form.Control>
                                </Form.Group>

                                <Form.Group
                                    className="description-event-info"
                                    controlId="description-event-info-Textarea"
                                >
                                    <Form.Label>Описание события:</Form.Label>
                                    <Form.Control as="textarea" rows={3}
                                        disabled
                                        readOnly
                                    >
                                        {event.objResult.description}
                                    </Form.Control>
                                </Form.Group>

                                <Form.Group
                                    className="start-event-info"
                                    controlId="start-event-info-Textarea"
                                >
                                    <Form.Label>Время начала:</Form.Label>
                                    <Form.Control as="textarea" rows={1}
                                        disabled
                                        readOnly
                                    >
                                        {TimeToString(startEvent)}
                                    </Form.Control>
                                </Form.Group>

                                <Form.Group
                                    className="end-event-info"
                                    controlId="end-event-info-Textarea"
                                >
                                    <Form.Label>Время окончания:</Form.Label>
                                    <Form.Control as="textarea" rows={1}
                                        disabled
                                        readOnly
                                    >
                                        {TimeToString(endEvent)}
                                    </Form.Control>
                                </Form.Group>
                            </Form>

                        </Modal.Body>
                    </>
                }
                <Modal.Body>


                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Выйти
                    </Button>
                    <Button variant="primary" onClick={deleteEvent}>
                        Удалить событие
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    )
}

export default EventInfo

