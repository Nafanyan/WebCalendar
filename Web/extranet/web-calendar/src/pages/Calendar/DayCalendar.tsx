import { FunctionComponent, useState, useEffect } from "react"
import { Card, Button } from "react-bootstrap"
import { fullDaysWeek } from "../../constants/DayOfWeek"
import AddEvent from "./actions-with-events/addEvent"
import "../../css/calendar/day-calendar.css"
import EventInfoDayMode from "./actions-with-events/eventInfoDayMode"
import { TimeToStringRequest } from "../../custom-function/TimeToString"
import { useTypedSelector } from "../../hooks/UseTypeSelector"
import { IEvent } from "../../models/IEvent"
import { IEventArray } from "../../models/IEventArray"
import { UserService } from "../../services/UserService"

export const DayCalendare: FunctionComponent = () => {
    const [dayEvents, setDayEvents] = useState<IEventArray>({ arrayEvents: new Array<IEvent>() });
    const { userId, day, month, year, reRender: nextRendering } = useTypedSelector(state => state.currentDay);

    useEffect(() => {
        const formingInfoInArray = async () => {
            const service: UserService = new UserService();
            let startEventStr: string = TimeToStringRequest(new Date(year, month - 1, day, 0, 0));
            let endEventStr: string = TimeToStringRequest(new Date(year, month - 1, day, 23, 59));
            events = await service.getEvent(userId, startEventStr, endEventStr);

            fillInArray();
        };

        const fillInArray = () => {
            if (events.length == 0) {
                let emptyEvents: IEvent[] = [];
                let emptyEvent: IEvent;
                emptyEvent = {
                    userId: userId,
                    name: "",
                    description: "",
                    startEvent: new Date(year, month - 1, day, 0, 0, 0),
                    endEvent: new Date(year, month - 1, day, 0, 0, 0)
                };
                emptyEvents.push(emptyEvent);
                setDayEvents({ arrayEvents: emptyEvents });
            }
            else {
                setDayEvents({ arrayEvents: events });
            }
        };

        let events: IEvent[] = [];

        formingInfoInArray();
    }, [userId, day, month, year, nextRendering]);

    return (<div>
        <Card className='events-of-day'>
            <Card.Header className='card-day-header-day-mode'>
                {fullDaysWeek[new Date(year, month - 1, day).getDay()]}
                <AddEvent day={new Date(year, month - 1, day)} />
            </Card.Header>

            <div className="scrollbar scrollbar-success">
                <Card.Body className='card-day'>
                    {dayEvents.arrayEvents.map((eventsDay, key) => (
                        eventsDay.name != "" &&
                        <EventInfoDayMode eventDate={eventsDay} key={key} />
                    ))}
                </Card.Body>
            </div>
        </Card>
    </div>)
}

export default DayCalendare;