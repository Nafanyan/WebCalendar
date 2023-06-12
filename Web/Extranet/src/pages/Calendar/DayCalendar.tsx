<<<<<<< HEAD
import React, { FunctionComponent, useEffect, useState } from 'react';
import { IEvent } from '../../models/IEvent';
import { IEventArray } from '../../models/IEventArray';
import { UserService } from '../../services/UserService';
import { TimeToString, TimeToStringRequest } from '../../CustomFunctions/TimeToString';
import { Button, Card } from 'react-bootstrap';

export interface DayCalendarProps {
    userId: number
    day: number,
    month: number
    year: number
}

export const DayCalendare: FunctionComponent<DayCalendarProps> = ({ userId, day, month, year }) => {
    const [events, setEvents] = useState<IEventArray>({
        arrayEvents: new Array<IEvent>()
    });

    useEffect(() => {

        const fetchEvents = async () => {
            const service: UserService = new UserService();
            let startEventStr: string = TimeToStringRequest(new Date(year, month, day, 0, 0));
            let endEventStr: string = TimeToStringRequest(new Date(year, month, day, 23, 59));
            setEvents({ arrayEvents: await service.getEvent(userId, startEventStr, endEventStr) });

            if (events.arrayEvents.length == 0) {
                let emptyEvents: IEvent[] = [];
                let emptyEvent: IEvent;
                emptyEvent = {
                    id: userId,
                    name: "",
                    description: "",
                    startEvent: new Date(year, month - 1, day, 0, 0, 0),
                    endEvent: new Date(year, month - 1, day, 0, 0, 0)
                }
                emptyEvents.push(emptyEvent);
                setEvents({ arrayEvents: emptyEvents });
            }
            console.log(events);
        };
        fetchEvents();
    }, [])

    return (<div>
        {events.arrayEvents.map((eventsDay, keyEvent) => (
            <Card className='day-of-months' key={keyEvent}>
                <Card.Header className='card-day-header'>
                    {new Date(eventsDay.startEvent.toString()).getDate()}
                    <button className='add-event-button'>+</button>
                </Card.Header>
                <Card.Body className='card-day-text'>
                    {eventsDay.name != "" &&
                        <Card.Text >
                            {eventsDay.name + " "}
                            {TimeToString(eventsDay.startEvent) + " - " + TimeToString(eventsDay.endEvent)}
                            {eventsDay.description != "" ? "\n" + eventsDay.description : ""}
                        </Card.Text>}
                </Card.Body>
            </Card>
        ))}
    </div>)
=======
import React, { FunctionComponent } from 'react';

export const DayCalendare : FunctionComponent = () => {
    return (<div></div>)
>>>>>>> 125585c430fe776b09da1b081f306bfe240cf94f
}

export default DayCalendare;