import { FunctionComponent, useState } from "react"
import { Modal, Button, Spinner } from "react-bootstrap"
import { useDispatch } from "react-redux"
import { IValidationResult } from "../../../models/IValidationResult"
import { IEventQueryResult } from "../../../models/query/Events/IEventQuery"
import { CurrentDayActionType } from "../../../models/type/currentDay"
import { EventService } from "../../../services/EventService"
import "../../../css/calendar/actions-with-events/event-info.css"
import { CanEditEventInfo, ShowEventInfo } from "./eventInfoAction"
import { TimeToStringRequest, TimeToStringCommand, TimeToString } from "../../../custom-utils/TimeToString"
import { useTypedSelector } from "../../../hooks/UseTypeSelector"

export interface EventInfoProps {
    startEvent: Date,
    endEvent: Date
}

export const EventInfo: FunctionComponent<EventInfoProps> = ({ startEvent, endEvent }) => {
    const { userId, reRender } = useTypedSelector(status => status.currentDay);
    const dispatch = useDispatch();

    const [show, setShow] = useState<boolean>(false);
    const [canEditEvent, setCanEditEvent] = useState<boolean>(false);
    const [response, setResponse] = useState<IValidationResult>({ isFail: false, error: "" });

    const [requestInProgress, setRequestInProgress] = useState<boolean>(false);

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
                isFail: false
            }
        }
    );

    const handleClose = () => {
        setShow(false)
    };

    const handleShow = async () => {
        setShow(true)
        const service: EventService = new EventService();
        let startEventStr: string = TimeToStringRequest(new Date(startEvent));
        let endEventStr: string = TimeToStringRequest(new Date(endEvent));
        setEvent(await service.get(userId, startEventStr, endEventStr));
    };

    const deleteEvent = async () => {
        setRequestInProgress(true);
        const service: EventService = new EventService();
        let startEventStr: string = TimeToStringCommand(new Date(startEvent), TimeToString(startEvent));
        let endEventStr: string = TimeToStringCommand(new Date(endEvent), TimeToString(endEvent));

        let result: IValidationResult = await service.delete(userId, {
            StartEvent: startEventStr,
            EndEvent: endEventStr
        });
        setResponse(result);
        if (!result.isFail) {
            dispatch({ type: CurrentDayActionType.FORCED_DEPENDENCY_RENDER, reRender: !reRender });
            handleClose();
        }
        setRequestInProgress(false);
    };

    const updateEvent = async () => {
        setRequestInProgress(true);
        const service: EventService = new EventService();
        let startEventStr: string = TimeToStringCommand(new Date(startEvent), TimeToString(event.objResult.startEvent));
        let endEventStr: string = TimeToStringCommand(new Date(endEvent), TimeToString(event.objResult.endEvent));

        let result: IValidationResult = await service.updateEvent(userId, {
            Name: event.objResult.name,
            Description: event.objResult.description,
            StartEvent: startEventStr,
            EndEvent: endEventStr
        })
        setResponse(result);
        if (!result.isFail) {
            dispatch({ type: CurrentDayActionType.FORCED_DEPENDENCY_RENDER, reRender: !reRender });
            handleClose();
            setCanEditEvent(false);
        }
        setRequestInProgress(false);
    }

    return (
        <>
            <Button variant="light" onClick={handleShow}>
                {TimeToString(startEvent) + " - " + TimeToString(endEvent)}
            </Button>

            <Modal show={show}
                onHide={() => { if (!canEditEvent) { handleClose() } }}>
                <Modal.Header closeButton>
                    <Modal.Title>Данные события</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    {response.isFail &&
                        <div className="alert alert-danger" role="alert">
                            {response.error}
                        </div>
                    }
                    {event.objResult.name != " " &&
                        <>
                            {canEditEvent ? (<CanEditEventInfo event={event} />) : (<ShowEventInfo event={event} />)}
                        </>
                    }
                </Modal.Body>

                <Modal.Footer>
                    {canEditEvent ? (
                        <>
                            <Button variant="secondary" onClick={() => setCanEditEvent(false)}>
                                Отменить
                            </Button>
                            {requestInProgress
                                ?
                                <Button variant="primary" disabled >
                                    <Spinner size="sm" />
                                    Сохранить
                                </Button> :
                                <Button variant="primary" onClick={updateEvent} >
                                    Сохранить
                                </Button>}
                        </>
                    ) : (
                        <>
                            <Button variant="secondary" onClick={() => setCanEditEvent(true)}>
                                Изменить
                            </Button>
                            {requestInProgress
                                ?
                                <Button variant="primary" disabled>
                                    <Spinner size="sm" />
                                    Удалить событие
                                </Button> :
                                <Button variant="primary" onClick={deleteEvent}>
                                    Удалить событие
                                </Button>
                            }
                        </>
                    )}
                </Modal.Footer>
            </Modal >
        </>
    )
}

export default EventInfo