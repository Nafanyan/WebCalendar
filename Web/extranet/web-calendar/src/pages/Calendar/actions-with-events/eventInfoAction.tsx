import { FunctionComponent, useState } from "react"
import { Form, Modal } from "react-bootstrap"
import { IEventQueryResult } from "../../../models/query/Events/IEventQuery"
import { TimeToString } from "../../../custom-utils/TimeToString"

interface ShowEventInfoProps {
    event: IEventQueryResult
}

export const ShowEventInfo: FunctionComponent<ShowEventInfoProps> = ({ event }) => {
    return (
            <Form>
                <Form.Group
                    className="name-event-info"
                    controlId="name-event-info-Textarea"
                >
                    <Form.Label>Название события:</Form.Label>
                    <Form.Control as="textarea" rows={1}
                        disabled
                        readOnly
                        value={event.objResult.name}
                    />
                </Form.Group>

                <Form.Group
                    className="description-event-info"
                    controlId="description-event-info-Textarea"
                >
                    <Form.Label>Описание события:</Form.Label>
                    <Form.Control as="textarea" rows={3}
                        disabled
                        readOnly
                        value={event.objResult.description}
                    />
                </Form.Group>

                <Form.Group
                    className="start-event-info"
                    controlId="start-event-info-Textarea"
                >
                    <Form.Label>Время начала:</Form.Label>
                    <Form.Control as="textarea" rows={1}
                        disabled
                        readOnly
                        value={TimeToString(event.objResult.startEvent)}
                    />
                </Form.Group>

                <Form.Group
                    className="end-event-info"
                    controlId="end-event-info-Textarea"
                >
                    <Form.Label>Время окончания:</Form.Label>
                    <Form.Control as="textarea" rows={1}
                        disabled
                        readOnly
                        value={TimeToString(event.objResult.endEvent)}
                    />
                </Form.Group>
            </Form>
    )
}

export const CanEditEventInfo: FunctionComponent<ShowEventInfoProps> = ({ event }) => {
    const [nameEvent, setNameEvent] = useState<string>(event.objResult.name);
    const [descriptionEvent, setDescriptionEvent] = useState<string>(event.objResult.description);
    return (
                <Form>
                    <Form.Group
                        className="name-event"
                        controlId="exampleForm.ControlTextarea1"
                    >
                        <Form.Label>Название события:</Form.Label>
                        <Form.Control as="textarea" rows={1}
                            onChange={n => {
                                event.objResult.name = n.target.value
                                setNameEvent(n.target.value)
                            }
                            }
                            value={nameEvent} />
                    </Form.Group>

                    <Form.Group
                        className="description-event"
                        controlId="exampleForm.ControlTextarea1"
                    >
                        <Form.Label>Описание события:</Form.Label>
                        <Form.Control as="textarea" rows={3}
                            onChange={d => {
                                event.objResult.description = d.target.value
                                setDescriptionEvent(d.target.value)
                            }
                            }
                            value={descriptionEvent} />
                    </Form.Group>

                    <Form.Group
                        className="start-event-info"
                        controlId="start-event-info-Textarea"
                    >
                        <Form.Label>Время начала:</Form.Label>
                        <Form.Control as="textarea" rows={1}
                            disabled
                            readOnly
                            value={TimeToString(event.objResult.startEvent)}
                        />
                    </Form.Group>

                    <Form.Group
                        className="end-event-info"
                        controlId="end-event-info-Textarea"
                    >
                        <Form.Label>Время окончания:</Form.Label>
                        <Form.Control as="textarea" rows={1}
                            disabled
                            readOnly
                            value={TimeToString(event.objResult.endEvent)}
                        />
                    </Form.Group>
                </Form>
    );
}