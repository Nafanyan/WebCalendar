import { FunctionComponent, useEffect, useState } from "react";
import { IDay } from "../../models/IDay";
import "../../css/calendar/chart-for-day.css"
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Doughnut } from "react-chartjs-2";
import { TimeToString } from "../../custom-utils/TimeToString";

export interface ChartForDayProps {
    day: IDay
};

ChartJS.register(ArcElement, Tooltip, Legend);

export const ChartForDay: FunctionComponent<ChartForDayProps> = ({ day }) => {
    const [labels, setLabels] = useState<string[]>([]);
    const [labelsTime, setLabelsTime] = useState<string[]>([]);
    const [timeEvents, setTimeEvents] = useState<number[]>([]);
    const [backgroundColor, setBackgroundColor] = useState<string[]>([]);
    const [borderColor, setBorderColor] = useState<string[]>([]);

    useEffect(() => {
        function TimeBeforyFirstEvent() {
            let nowDay: Date = new Date(day.date);
            let startOfDayTime: Date = new Date(nowDay.setHours(0, 0, 0));

            if (new Date(day.arrayEvents[0].startEvent).getTime() != startOfDayTime.getTime()) {
                localLabels.push("Свободное время: " + TimeToString(startOfDayTime) + " - " + TimeToString(day.arrayEvents[0].startEvent));
                localLabelsTime.push(TimeToString(startOfDayTime) + " - " + TimeToString(day.arrayEvents[0].startEvent));
                localTimeEvents.push((new Date(day.arrayEvents[0].startEvent).getTime() - new Date(startOfDayTime).getTime()) / 60000);
                localBackgroundColor.push("rgba(75, 192, 192, 0.2)");
                localBorderColor.push("rgba(75, 192, 192, 1)");
            }
        };

        function TimeBetweenEvents(endFirstEvent: Date, startSecondEvent: Date) {
            localLabels.push("Свободное время: " + TimeToString(endFirstEvent) + " - " + TimeToString(startSecondEvent));
            let timeEvents: number = (new Date(startSecondEvent).getTime() - new Date(endFirstEvent).getTime()) / 60000;
            localTimeEvents.push(timeEvents);
            localBackgroundColor.push("rgba(75, 192, 192, 0.2)");
            localBorderColor.push("rgba(75, 192, 192, 1)");
        };

        function TimeAfterLastEvent(endLastEvent: Date) {
            let nowDay: Date = new Date(day.date);
            let endOfDayTime = new Date(nowDay.setHours(23, 59));

            if (new Date(endLastEvent).getTime() != new Date(endOfDayTime).getTime()) {
                localLabels.push("Свободное время: " + TimeToString(endLastEvent) + " - " + TimeToString(endOfDayTime));
                localTimeEvents.push((new Date(endOfDayTime).getTime() - new Date(endLastEvent).getTime()) / 60000);
                localBackgroundColor.push("rgba(75, 192, 192, 0.2)");
                localBorderColor.push("rgba(75, 192, 192, 1)");
            }
        };

        function EventTiming() {
            let startEvent: Date = new Date();
            let endEvent: Date = new Date();
            let nameEvent: string = "";
            let timeEvents: number;

            for (let i = 0; i < day.arrayEvents.length; i++) {
                startEvent = new Date(day.arrayEvents[i].startEvent);
                endEvent = new Date(day.arrayEvents[i].endEvent);
                nameEvent = day.arrayEvents[i].name.length > 20 ? day.arrayEvents[i].name.substring(0, 20) + "... " : day.arrayEvents[i].name + " ";

                localLabels.push(nameEvent + ": " + TimeToString(startEvent) + " - " + TimeToString(endEvent))
                timeEvents = (new Date(endEvent).getTime() - new Date(startEvent).getTime()) / 60000;
                localTimeEvents.push(timeEvents);
                localBackgroundColor.push(timeEvents >= 240 ? "rgba(255, 99, 132, 0.2)" : "rgba(255, 159, 64, 0.2)");
                localBorderColor.push(timeEvents >= 240 ? "rgba(255, 99, 132, 1)" : "rgba(255, 159, 64, 1)");

                if (i + 1 != day.arrayEvents.length) {
                    TimeBetweenEvents(new Date(endEvent), new Date(day.arrayEvents[i + 1].startEvent));
                }
            }
            TimeAfterLastEvent(new Date(endEvent));
        };

        function FreeDay() {
            let nowDay: Date = new Date(day.date);
            let startOfDayTime: Date = new Date(nowDay.setHours(0, 0, 0));
            let endOfDayTime = new Date(nowDay.setHours(23, 59));

            localLabels.push("Свободное время: " + TimeToString(startOfDayTime) + " - " + TimeToString(endOfDayTime));
            localTimeEvents.push((new Date(endOfDayTime).getTime() - new Date(startOfDayTime).getTime()) / 60000);
            localBackgroundColor.push("rgba(75, 192, 192, 0.2)");
            localBorderColor.push("rgba(75, 192, 192, 1)");
        };

        let localLabels: string[] = [];
        let localLabelsTime: string[] = [];
        let localTimeEvents: number[] = [];
        let localBackgroundColor: string[] = [];
        let localBorderColor: string[] = [];

        if (day.arrayEvents.length != 0) {
            TimeBeforyFirstEvent();
            EventTiming();
        }
        else {
            FreeDay();
        }

        setLabels(localLabels);
        setLabelsTime(localLabelsTime);
        setTimeEvents(localTimeEvents);
        setBackgroundColor(localBackgroundColor);
        setBorderColor(localBorderColor);
    }, [day]);

    const data = {
        labels: labels,
        datasets: [
            {
                label: "Продолжительность в минутах",
                data: timeEvents,
                backgroundColor: backgroundColor,
                borderColor: borderColor,
                borderWidth: 1,
            },
        ],
    };

    return (<Doughnut data={data} options={{
        responsive: true,
        maintainAspectRatio: true,
        plugins: {
            legend: {
                display: false
            }
        }
    }
    } />);
}