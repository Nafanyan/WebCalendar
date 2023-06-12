<<<<<<< HEAD
export interface IEvent {
    id: number,
    name: string,
    description: string,
    startEvent: Date,
    endEvent: Date
=======
import { IDate } from "./IDate";

export interface IEvent {
id : number,
record : string,
description: string,
startEvent: IDate,
endEvent: IDate
>>>>>>> 125585c430fe776b09da1b081f306bfe240cf94f
}