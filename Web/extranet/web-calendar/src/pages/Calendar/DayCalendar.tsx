import { FunctionComponent, useState, useEffect } from "react";
import { Card, Button } from "react-bootstrap";
import { fullDaysWeek } from "../../constants/DayOfWeek";
import AddEvent from "./actions-with-events/addEvent";
import "../../css/calendar/day-calendar.css";
import EventInfoDayMode from "./actions-with-events/eventInfoDayMode";
import { TimeToStringRequest } from "../../custom-utils/TimeToString";
import { useTypedSelector } from "../../hooks/UseTypeSelector";
import { IEvent } from "../../models/IEvent";
import { IDay } from "../../models/IDay";
import { UserService } from "../../services/UserService";
import { ChartForDay } from "./ChartForDay";

export const DayCalendare: FunctionComponent = () => {
    const { userId, day, month, year, reRender } = useTypedSelector(state => state.currentDay);
    const [dayEvents, setDayEvents] = useState<IDay>({ arrayEvents: [], date: new Date(year, month, day, 0, 0, 0) });


    useEffect(() => {
        const dataInitialization = async () => {
            const service: UserService = new UserService();
            let startEventStr: string = TimeToStringRequest(new Date(year, month, day, 0, 0));
            let endEventStr: string = TimeToStringRequest(new Date(year, month, day, 23, 59));
            events = await service.getEvent(userId, startEventStr, endEventStr);

            fillInArray();
        };

        const fillInArray = () => {
            if (events.length == 0) {
                setDayEvents({ date: new Date(year, month, day, 0, 0, 0), arrayEvents: [] });
            }
            else {
                setDayEvents({ date: new Date(year, month, day, 0, 0, 0), arrayEvents: events });
            }
        };

        let events: IEvent[] = [];
        dataInitialization();
    }, [userId, day, month, year, reRender]);

    return (<div className="flexbox-container">
        <div>
            <Card className='events-of-day'>
                <Card.Header className='card-day-header-day-mode'>
                    {fullDaysWeek[new Date(year, month, day).getDay()]}
                    <AddEvent day={new Date(year, month, day)} />
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
        </div>
        <div className="div-chart-events">
            <p className="name-diagram">Диаграмма занятости в течении дня</p>
            <ChartForDay day={dayEvents} />
        </div>
    </div>
    )
}

export default DayCalendare;