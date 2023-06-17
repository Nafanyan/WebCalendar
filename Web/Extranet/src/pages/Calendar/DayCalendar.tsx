import { FunctionComponent, useEffect, useState } from 'react';
import { IEvent } from '../../models/IEvent';
import { IEventArray } from '../../models/IEventArray';
import { UserService } from '../../services/UserService';
import { Button, Card } from 'react-bootstrap';
import { TimeToStringRequest, TimeToString } from '../../custom-functions/TimeToString';
import { fullDaysWeek } from '../../constants/DayOfWeek';
import "../../css/day-calendar.css"
import { useTypedSelector } from '../../hooks/useTypeSelector';
import AddEvent from '../AddEvent';

export const DayCalendare: FunctionComponent = () => {
    const [dayEvents, setDayEvents] = useState<IEventArray>({ arrayEvents: new Array<IEvent>() });
    const { userId, day, month, year, nextRendering } = useTypedSelector(state => state.currentDay)
    useEffect(() => {
        const createEvents = async () => {
            const service: UserService = new UserService();
            let startEventStr: string = TimeToStringRequest(new Date(year, month - 1, day, 0, 0));
            let endEventStr: string = TimeToStringRequest(new Date(year, month - 1, day, 23, 59));
            let events: IEvent[] = await service.getEvent(userId, startEventStr, endEventStr);

            if (events.length == 0) {
                let emptyEvents: IEvent[] = [];
                let emptyEvent: IEvent;
                emptyEvent = {
                    userId: userId,
                    name: "",
                    description: "",
                    startEvent: new Date(year, month - 1, day, 0, 0, 0),
                    endEvent: new Date(year, month - 1, day, 0, 0, 0)
                }
                emptyEvents.push(emptyEvent);
                setDayEvents({ arrayEvents: emptyEvents });
            }
            else {
                setDayEvents({ arrayEvents: events });
            }
        };

        createEvents();
    }, [userId, day, month, year, nextRendering])

    return (<div>
        <Card className='events-of-day'>
            <Card.Header className='card-day-header-day-mode'>
                {fullDaysWeek[new Date(year, month - 1, day).getDay()]}
                <AddEvent day={new Date(year, month - 1, day)} />
            </Card.Header>
            
            <div className="scrollbar scrollbar-success">
                <Card.Body className='card-day'>
                    {dayEvents.arrayEvents.map((eventsDay, keyEvent) => (
                        eventsDay.name != "" &&
                        <Button variant="outline-success" id={'event-info'} key={keyEvent}>
                            <Card.Text className='event-name-time'>
                                {eventsDay.name + " "}
                                {TimeToString(eventsDay.startEvent) + " - " + TimeToString(eventsDay.endEvent)}
                            </Card.Text>
                            <Card.Text className='event-description'>
                                {eventsDay.description != "" ? "\n" + eventsDay.description : ""}
                            </Card.Text>
                        </Button>
                    ))}
                </Card.Body>
            </div>
        </Card>
    </div>)
}

export default DayCalendare;