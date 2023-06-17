import { IValidationResult } from "../models/IValidationResult";
import { IAddEvent } from "../models/command/IAddEvent";
import { IDeleteEvent } from "../models/command/IDeleteEvent";
import { IEventQueryResult } from "../models/query/IEventQuery";
import AxiosDefault from "./AxiosDefault";

export class EventService {
    async get(id: number, startPeriod: string, endPeriod: string): Promise<IEventQueryResult> {
        return (await AxiosDefault.get<IEventQueryResult>("Users/" + id + "/Events?startEvent=" + startPeriod + "&endEvent=" + endPeriod)).data;
    }

    async delete(id: number, date: IDeleteEvent): Promise<IValidationResult> {
        let validationResult: IValidationResult = (await AxiosDefault.delete("Users/" + id + "/Events", {data: date})).data;
        return validationResult;
    }

    async addEvent(id: number, date: IAddEvent): Promise<IValidationResult> {
        let validationResult: IValidationResult = (await AxiosDefault.post("Users/" + id + "/Events", date)).data;
        return validationResult;
    }
}