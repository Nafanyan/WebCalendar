import { FunctionComponent, useState } from "react"
import { Modal, Button, Form, Card } from "react-bootstrap"
import { useDispatch } from "react-redux"
import { TimeToStringRequest, TimeToStringCommand, TimeToString } from "../../../custom-functions/TimeToString"
import { useTypedSelector } from "../../../hooks/useTypeSelector"
import { IValidationResult } from "../../../models/IValidationResult"
import { IEventQueryResult } from "../../../models/query/IEventQuery"
import { CurrentDayActionType } from "../../../models/type/currentDay"
import { EventService } from "../../../services/EventService"
import "../../../css/calendar/actions-with-events/event-info-day-mode.css"
import { IEvent } from "../../../models/IEvent"

export interface EventInfoDayModeProps {
    eventDate: IEvent
}

export const EventInfoDayMode: FunctionComponent<EventInfoDayModeProps> = ({ eventDate }) => {
    const { userId, nextRendering } = useTypedSelector(status => status.currentDay);
    const dispatch = useDispatch();

    const [show, setShow] = useState(false);

    const [event, setEvent] = useState<IEventQueryResult>(
        {
            objResult: {
                userId: userId,
                name: " ",
                description: " ",
                startEvent: eventDate.startEvent,
                endEvent: eventDate.endEvent
            },
            validationResult: {
                IsFail: false,
                Error: null
            }
        }
    );

    const handleClose = () => {
        setShow(false)
    };

    const handleShow = async () => {
        setShow(true)
        const service: EventService = new EventService();
        let startEventStr: string = TimeToStringRequest(new Date(eventDate.startEvent));
        let endEventStr: string = TimeToStringRequest(new Date(eventDate.endEvent));
        setEvent(await service.get(userId, startEventStr, endEventStr));
    };

    const deleteEvent = async () => {
        const service: EventService = new EventService();
        let startEventStr: string = TimeToStringCommand(new Date(eventDate.startEvent), TimeToString(eventDate.startEvent));
        let endEventStr: string = TimeToStringCommand(new Date(eventDate.endEvent), TimeToString(eventDate.endEvent));

        let result: IValidationResult = await service.delete(userId, {
            StartEvent: startEventStr,
            EndEvent: endEventStr
        });

        dispatch({ type: CurrentDayActionType.FORCED_DEPENDENCY_RENDER, nextRendering: !nextRendering });
        handleClose();
    };

    return (
        <>
            <Button variant="outline-success" id={'event-info'} onClick={handleShow}>
                <Card.Text className='event-name-time'>
                    {eventDate.name + " "}
                    {TimeToString(eventDate.startEvent) + " - " + TimeToString(eventDate.endEvent)}
                </Card.Text>
                <Card.Text className='event-description'>
                    {eventDate.description != "" ? "\n" + eventDate.description : ""}
                </Card.Text>
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
                                        {TimeToString(eventDate.startEvent)}
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
                                        {TimeToString(eventDate.endEvent)}
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

export default EventInfoDayMode

