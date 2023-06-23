import { IValidationResult } from "../models/IValidationResult";
import { IAddEvent } from "../models/command/IAddEvent";
import { IDeleteEvent } from "../models/command/IDeleteEvent";
import { IUpdateEvent } from "../models/command/IUpdateEvent";
import { IEventQueryResult } from "../models/query/IEventQuery";
import AxiosDefault from "./AxiosDefault";

export class EventService {
    async get(id: number, startPeriod: string, endPeriod: string): Promise<IEventQueryResult> {
        return (await AxiosDefault.get<IEventQueryResult>("Users/" + id + "/Events?startEvent=" + startPeriod + "&endEvent=" + endPeriod)).data;
    }

    async delete(id: number, date: IDeleteEvent): Promise<IValidationResult> {
        let response: IValidationResult = await AxiosDefault.delete("Users/" + id + "/Events", { data: date }).then((response) => {
            return response.data;
        }).catch(error => {
            return error.response.data.validationResult
        })
        return response;
    }

    async addEvent(id: number, date: IAddEvent): Promise<IValidationResult> {
        let response: IValidationResult = await AxiosDefault.post("Users/" + id + "/Events", date).then((response) => {
            return response.data;
        }).catch(error => {
            return error.response.data.validationResult
        })
        return response;

    }

    async updateEvent(id: number, date: IUpdateEvent): Promise<IValidationResult> {
        let response: IValidationResult = await AxiosDefault.put("Users/" + id + "/Events", date).then((response) => {
            return response.data;
        }).catch(error => {
            return error.response.data
        })
        return response;
    }
}