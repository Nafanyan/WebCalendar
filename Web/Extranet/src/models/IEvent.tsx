import { IDate } from "./IDate";

export interface IEvent {
id : number,
record : string,
description: string,
startEvent: IDate,
endEvent: IDate
}