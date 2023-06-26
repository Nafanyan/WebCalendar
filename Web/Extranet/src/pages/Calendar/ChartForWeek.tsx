import React, { FunctionComponent, useEffect, useState } from 'react';
import { IEventArray } from '../../models/IEventArray';
import {VictoryChart, VictoryLine, VictoryStack, VictoryTheme } from 'victory';
import { shortDaysWeek } from '../../constants/DayOfWeek';
import { Accordion } from 'react-bootstrap';
import "../../css/calendar/chart-for-week.css"

export interface ChartForWeekProps {
    events: IEventArray[][]
}

interface ChartDateTime {
    x: string,
    y: number
}

export const ChartForWeek: FunctionComponent<ChartForWeekProps> = ({ events }) => {
    const [chartData, setChartData] = useState<ChartDateTime[]>([])
    const [max, setMax] = useState<number>(0);

    useEffect(() => {
        let newCharData: ChartDateTime[] = [{ x: " ", y: 0 }];
        let max: number = 0;
        events.map((week, keyWeek) => {
            week.map((day, keyDay) => {
                let busyTime: number = 0;
                day.arrayEvents.map((nowEvent, keyNowEvent) => {
                    busyTime += (new Date(nowEvent.endEvent).getTime() - new Date(nowEvent.startEvent).getTime()) / 3600000;
                })
                if (busyTime > max) {
                    max = busyTime + 1;
                }
                newCharData.push({ x: shortDaysWeek[keyDay], y: busyTime })
            })
        })
        setChartData(newCharData);
        setMax(max);
    }, [events]);

    return (
        !IsEmpty(chartData) ?
            <Accordion>
                <Accordion.Header className='header-accordion'>Employment schedule</Accordion.Header>
                <Accordion.Body>
                    <VictoryChart maxDomain={{ x: 7, y: max }}>
                        <VictoryStack>
                            <VictoryLine name="TimeLime"
                                theme={VictoryTheme.grayscale}
                                data={chartData}
                                x="x"
                                y="y"
                            />
                        </VictoryStack>
                    </VictoryChart>
                </Accordion.Body>
            </Accordion>
            :
            <></>
    );
}

function IsEmpty(array: ChartDateTime[]): boolean {
    let isEmpty: boolean = true;
    array.forEach(element => {
        if (element.y > 0) {
            isEmpty = false;
        }
    });
    return isEmpty;
}