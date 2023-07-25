import { FunctionComponent, useEffect, useState } from 'react';
import { IDay } from '../../models/IDay';
import { shortDaysWeek } from '../../constants/DayOfWeek';
import { Accordion } from 'react-bootstrap';
import "../../css/calendar/chart-for-week.css"
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';
import { Line } from 'react-chartjs-2';

ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
);

export const options = {
    responsive: true,
    plugins: {
        legend: {
            position: 'top' as const
        },
        title: {
            display: false,
            text: 'Chart.js Line Chart',
        },
    },
};

export interface ChartForWeekProps {
    weeks: IDay[][]
};

interface ChartDateTime {
    x: string,
    y: number
};

export const ChartForWeek: FunctionComponent<ChartForWeekProps> = ({ weeks }) => {
    const [chartData, setChartData] = useState<ChartDateTime[]>([])

    useEffect(() => {
        let newCharData: ChartDateTime[] = [{ x: " ", y: 0 }];
        let max: number = 0;
        weeks.map((week, keyWeek) => {
            week.map((day, keyDay) => {
                let busyTime: number = 0;
                day.arrayEvents.map((nowEvent, keyNowEvent) => {
                    busyTime += (new Date(nowEvent.endEvent).getTime() - new Date(nowEvent.startEvent).getTime()) / 3600000;
                })
                newCharData.push({ x: shortDaysWeek[keyDay], y: busyTime });
            })
        })
        setChartData(newCharData);
    }, [weeks]);

    const data = {
        shortDaysWeek,
        datasets: [
            {
                label: 'Занятость в часах',
                data: chartData,
                borderColor: 'rgb(75, 192, 192)',
                backgroundColor: 'rgba(75, 192, 192, 0.5)',
            },
        ],
    };

    return (
        !IsEmpty(chartData) ?
            <Accordion>
                <Accordion.Header className='header-accordion'>График занятости в течении недели</Accordion.Header>
                <Accordion.Body>
                    <Line options={options} data={data} />;
                </Accordion.Body>
            </Accordion>
            :
            <></>
    );
};

function IsEmpty(array: ChartDateTime[]): boolean {
    let isEmpty: boolean = true;
    array.forEach(element => {
        if (element.y > 0) {
            isEmpty = false;
        }
    });
    return isEmpty;
};