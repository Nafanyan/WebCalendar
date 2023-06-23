import { FunctionComponent, useState } from "react"
import { Modal, Button, Form } from "react-bootstrap"
import { useDispatch } from "react-redux"
import { TimeToStringCommand } from "../../../custom-functions/TimeToString"
import { useTypedSelector } from "../../../hooks/useTypeSelector"
import { IValidationResult } from "../../../models/IValidationResult"
import { CurrentDayActionType } from "../../../models/type/currentDay"
import { EventService } from "../../../services/EventService"
import "../../../css/calendar/actions-with-events/add-event.css";

export interface AddEventProps {
    day: Date
}

export const AddEvent: FunctionComponent<AddEventProps> = ({ day }) => {
    const { userId, nextRendering } = useTypedSelector(state => state.currentDay);
    const dispatch = useDispatch();
    const [show, setShow] = useState<boolean>(false);
    const [response, setResponse] = useState<IValidationResult>({ isFail: false, error: "" });

    const [nameEvent, setNameEvent] = useState<string>("");
    const [descriptionEvent, setDescriptionEvent] = useState<string>("");
    const [startEvent, setStartEvent] = useState<string>('00:00');
    const [endEvent, setEndEvent] = useState<string>('00:00');

    const handleClose = () => {
        setShow(false)
        setNameEvent("")
        setDescriptionEvent("")
        setStartEvent('00:00')
        setEndEvent('00:00')
        setResponse({error: "", isFail: false})
    }
    const handleShow = () => setShow(true);

    const handleAdd = async () => {
        const service: EventService = new EventService();
        let startEventStr: string = TimeToStringCommand(new Date(day), startEvent);
        let endEventStr: string = TimeToStringCommand(new Date(day), endEvent);

        let result: IValidationResult = await service.addEvent(userId, {
            Name: nameEvent,
            Description: descriptionEvent,
            StartEvent: startEventStr,
            EndEvent: endEventStr
        })
        setResponse(result);
        if (!result.isFail) {
            dispatch({ type: CurrentDayActionType.FORCED_DEPENDENCY_RENDER, nextRendering: !nextRendering });
            handleClose();
        }
    }

    return (
        <>
            <button className='add-event-button'
                onClick={handleShow}>+</button>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Введите данные события</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                {response.isFail &&
                    <div className="alert alert-danger" role="alert">
                        {response.error}
                    </div>
                }
                    <Form>
                        <Form.Group
                            className="name-event"
                            controlId="exampleForm.ControlTextarea1"
                        >
                            <Form.Label>Название события:</Form.Label>
                            <Form.Control as="textarea" rows={1}
                                onChange={n => setNameEvent(n.target.value)} />
                        </Form.Group>

                        <Form.Group
                            className="description-event"
                            controlId="exampleForm.ControlTextarea1"
                        >
                            <Form.Label>Описание события:</Form.Label>
                            <Form.Control as="textarea" rows={3} onChange={d => setDescriptionEvent(d.target.value)} />
                        </Form.Group>

                        <div className="form-group row">
                            <label htmlFor="start-event-time-input" className="col-xs-2 col-form-label">Время начала</label>
                            <div className="start-event-div">
                                <input
                                    type="time"
                                    onChange={se => setStartEvent(se.target.value)}
                                    className="start-event"
                                    id="start-event-input"
                                    value={startEvent}>
                                </input>
                            </div>
                        </div>

                        <div className="form-group row">
                            <label htmlFor="end-event-time-input" className="col-xs-2 col-form-label">Время окончания</label>
                            <div className="end-event-div">
                                <input
                                    type="time"
                                    onChange={se => setEndEvent(se.target.value)}
                                    className="end-event"
                                    id="end-event-input"
                                    value={endEvent}>
                                </input>
                            </div>
                        </div>
                    </Form>

                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Отмена
                    </Button>
                    <Button variant="primary" onClick={handleAdd}>
                        Добавить событие
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    )
}

export default AddEvent