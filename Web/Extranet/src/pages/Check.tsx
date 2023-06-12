import { FunctionComponent, useEffect, useState } from "react";
import { IEvent } from "../models/IEvent";
import { UserService } from "../services/UserService";

export interface MonthCalendarProps {
    userId: number
    mounth: number
    year: number
}

interface IEventWithDate {
    id: number,
    name: string,
    description: string,
    startEvent: Date,
    endEvent: Date
}

function MapIEvent(event: IEvent) {
    let eventWithDate: IEventWithDate = {
        id: event.id,
        name: event.name,
        description: event.description,
        startEvent: new Date(event.startEvent),
        endEvent: new Date(event.endEvent)
    };
    return eventWithDate;
}

export const Check: FunctionComponent<MonthCalendarProps> = ({userId, mounth, year}) => {
    const [event3DArrayWithDate, setEvent3DArrayWithDate] = useState<IEventWithDate[][][]>([]);
    let dayInMounth: number[] = [28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31, 31];
    let nowDate: Date = new Date();

    useEffect(() => {
        const fetchEvent = async () => {
            const service: UserService = new UserService();

            let numDayOfMounth: number = dayInMounth[mounth];
            if (mounth == 2 && year % 4 == 0) {
                numDayOfMounth = 29;
            }
            let startEventStr: string = "01/" + mounth + "/" + year + " 00:00:00";
            let endEventStr: string = numDayOfMounth + "/" + mounth + "/" + year + " 00:00:00";
            let events: IEvent[] = await service.getEvent(userId, startEventStr, endEventStr);

            if (nowDate.getMonth() == 2 && nowDate.getFullYear() % 4 == 0) {
                numDayOfMounth = 29;
            }
            
            let event2DArrayWithDate: IEventWithDate[][] = [];
            let dayWithEvent: IEventWithDate;
            for (let i = 0; i < numDayOfMounth; i ++){
                let emptyEvent: IEventWithDate = {
                    id: 0,
                    name: "",
                    description: "",
                    startEvent: new Date(year, mounth - 1, i + 1),
                    endEvent: new Date(year, mounth - 1, i + 1)  
                }
                console.log(emptyEvent);
                event2DArrayWithDate.push([]);
                event2DArrayWithDate[i].push(emptyEvent);
            }

            for (let i = 0; i < events.length; i++){
                dayWithEvent = MapIEvent(events[i]);
                event2DArrayWithDate[dayWithEvent.startEvent.getDate() - 1].push(dayWithEvent);
            }
            
            let event3DArrayWithDate: IEventWithDate[][][] = [];
            for (let i = 0; i < numDayOfMounth; i += 7)
            {
                event3DArrayWithDate.push(event2DArrayWithDate.slice(i, i + 7));
            }
            setEvent3DArrayWithDate(event3DArrayWithDate);
        }
        fetchEvent()
    }, [])

    return <div>
        {event3DArrayWithDate.map(( dayWeek ) =>
        ( 
            dayWeek.map( (day) => (
                day.map( (dayEvent) => (
                    <p> {dayEvent.startEvent.getDate()} 
                    <a>
                        {dayEvent.name}
                    </a>
                    <a>
                    {dayEvent.description}
                    </a>
                    
                    <a>{dayEvent.startEvent.getHours()} : {dayEvent.startEvent.getMinutes().toString()}</a>
                    -
                    <a>{dayEvent.endEvent.getHours()} : {dayEvent.endEvent.getMinutes()}</a>
                    </p>
                ) )
            ) )
        )
        )}
    </div>
}