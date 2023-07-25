import { CheckToken } from "../custom-utils/CheckToken";
import { IValidationResult } from "../models/IValidationResult";
import { IAddEvent } from "../models/command/Events/IAddEvent";
import { IDeleteEvent } from "../models/command/Events/IDeleteEvent";
import { IUpdateEvent } from "../models/command/Events/IUpdateEvent";
import { IEventQueryResult } from "../models/query/Events/IEventQuery";
import AxiosBases from "./AxiosBases";

export class EventService {
    async get(id: number, startPeriod: string, endPeriod: string): Promise<IEventQueryResult> {
        await CheckToken();

        let response: IEventQueryResult = await AxiosBases.get<IEventQueryResult>(
            "Api/Users/" + id + "/Events?startEvent=" + startPeriod + "&endEvent=" + endPeriod,
            { headers: { "Access-Token": (localStorage.getItem('access-token') + "").replaceAll("\"", "") } })
            .then((response) => {
                return response.data;
            }).catch(error => {
                return error.response.data
            })
        return response;
    }

    async addEvent(id: number, date: IAddEvent): Promise<IValidationResult> {
        await CheckToken();

        let response: IValidationResult = await AxiosBases.post("Api/Users/" + id + "/Events", date,
            { headers: { "Access-Token": (localStorage.getItem('access-token') + "").replaceAll("\"", "") } })
            .then((response) => {
                return response.data;
            }).catch(error => {
                return error.response.data.validationResult
            })
        return response;
    }

    async delete(id: number, date: IDeleteEvent): Promise<IValidationResult> {
        await CheckToken();

        let response: IValidationResult = await AxiosBases.delete("Api/Users/" + id + "/Events",
            { data: date, headers: { "Access-Token": (localStorage.getItem('access-token') + "").replaceAll("\"", "") } })
            .then((response) => {
                return response.data;
            }).catch(error => {
                return error.response.data.validationResult
            })
        return response;
    }

    async updateEvent(id: number, date: IUpdateEvent): Promise<IValidationResult> {
        await CheckToken();
        
        let response: IValidationResult = await AxiosBases.put("Api/Users/" + id + "/Events", date,
            { headers: { "Access-Token": (localStorage.getItem('access-token') + "").replaceAll("\"", "") } })
            .then((response) => {
                return response.data;
            }).catch(error => {
                return error.response.data
            })
        return response;
    }
}