import { FunctionComponent, useState } from "react";
import { Button, Form, Modal } from "react-bootstrap";
import "../css/add-event.css"
import { fullDaysWeek } from "../constants/DayOfWeek";
import { IEvent } from "../models/query/IEvent";
import { UserService } from "../services/UserService";
import { TimeToStringCommand, TimeToStringRequest } from "../custom-functions/TimeToString";
import { IValidationResult } from "../models/command/IValidationResult";

export interface AddEventProps {
    userId: number;
    day: Date;
}

export const AddEvent: FunctionComponent<AddEventProps> = ({ userId, day }) => {
    const [show, setShow] = useState(false);

    const [nameEvent, setNameEvent] = useState<string>("");
    const [descriptionEvent, setDescriptionEvent] = useState<string>("");
    const [startEvent, setStartEvent] = useState<string>('00:00');
    const [endEvent, setEndEvent] = useState<string>('00:00');

    const handleClose = () => {
        setShow(false);
        setNameEvent("");
        setDescriptionEvent("");
        setStartEvent('00:00');
        setEndEvent('00:00');
    }
    const handleShow = () => setShow(true);
    const handleAdd = async() => {
        const service: UserService = new UserService();
        let startEventStr: string = TimeToStringCommand(new Date(day), startEvent);
        let endEventStr: string = TimeToStringCommand(new Date(day), endEvent);

        let result: IValidationResult =  await service.addEvent(userId, {
            Name: nameEvent,
            Description: descriptionEvent,
            StartEvent: startEventStr,
            EndEvent: endEventStr
        });
        handleClose();
    }

    // console.log("name ", nameEvent);
    // console.log("desriptionEvent ", desriptionEvent);
    // console.log("startEvent ", startEvent);
    // console.log("endEvent ", endEvent);

    return (
        <>
            <button className='add-event-button'
                onClick={handleShow}>+</button>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title> {fullDaysWeek[new Date(day).getDay()]}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group
                            className="name-event"
                            controlId="exampleForm.ControlTextarea1"
                        >
                            <Form.Label>Название события:</Form.Label>
                            <Form.Control as="textarea" rows={1} 
                            onChange={n => setNameEvent(n.target.value)}/>
                        </Form.Group>

                        <Form.Group
                            className="description-event"
                            controlId="exampleForm.ControlTextarea1"
                        >
                            <Form.Label>Описание события:</Form.Label>
                            <Form.Control as="textarea" rows={3} onChange={d => setDescriptionEvent(d.target.value)}/>
                        </Form.Group>

                        <div className="form-group row">
                            <label htmlFor="start-event-time-input" className="col-xs-2 col-form-label">Время начала</label>
                            <div className="start-event-div">
                                <input
                                    type="time"
                                    onChange={se => setStartEvent(se.target.value)}
                                    className="start-event"
                                    id="start-event-input">
                                    
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
                                    id="end-event-input">
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
    );
}

export default AddEvent;