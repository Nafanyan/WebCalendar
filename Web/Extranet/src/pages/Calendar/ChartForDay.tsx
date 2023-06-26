import { FunctionComponent, useState, useEffect } from "react";
import { Accordion } from "react-bootstrap";
import { VictoryChart } from "victory-chart";
import { VictoryTheme } from "victory-core";
import { VictoryLine } from "victory-line";
import { VictoryStack } from "victory-stack";
import { shortDaysWeek } from "../../constants/DayOfWeek";
import { IEvent } from "../../models/IEvent";
import { TimeToString } from "../../custom-functions/TimeToString";
import { VictoryHistogram } from "victory-histogram";
import { IEventArray } from "../../models/IEventArray";
import { VictoryArea } from "victory-area";

interface ChartDateTime {
    x: string,
    y: number
}

export const ChartForDay: FunctionComponent<IEventArray> = ({ arrayEvents }) => {
    const [chartData, setChartData] = useState<ChartDateTime[]>([])

    useEffect(() => {
        let newCharData: ChartDateTime[] = [{ x: " ", y: 0 }];
        let max: number = 0;
        let busyTime: number = 0;
        arrayEvents.map((nowEvents, key) => {
            let dateString: string = TimeToString(new Date(nowEvents.startEvent)) + " - " + TimeToString(new Date(nowEvents.endEvent));

        })
        // events.arrayEvents.map((nowEvent, keyNowEvent) => {
        //     busyTime += (new Date(nowEvent.endEvent).getTime() - new Date(nowEvent.startEvent).getTime()) / 3600000;
        // })
        // if (busyTime > max) {
        //     max = busyTime + 1;
        // }
        // newCharData.push({ x: shortDaysWeek[keyDay], y: busyTime })
        setChartData(newCharData);
    }, [arrayEvents]);

    return (
        true ?
            <Accordion>
                <Accordion.Header className='header-accordion'>Employment schedule</Accordion.Header>
                <Accordion.Body>
                    <VictoryChart>
                        <VictoryStack>
                            <VictoryArea
                                data={[{ x: "a", y: 2 }, { x: "b", y: 3 }, { x: "c", y: 5 }]}
                            />
                            <VictoryArea
                                data={[{ x: "a", y: 1 }, { x: "b", y: 4 }, { x: "c", y: 5 }]}
                            />
                            <VictoryArea
                                data={[{ x: "a", y: 3 }, { x: "b", y: 2 }, { x: "c", y: 6 }]}
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