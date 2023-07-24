import { CheckToken } from "../custom-utils/CheckToken";
import { IEvent } from "../models/IEvent";
import { IValidationResult } from "../models/IValidationResult";
import { IDeleteUser } from "../models/command/Users/IDeleteUser";
import { IRegistrateUser } from "../models/command/Users/IRegistrateUser";
import { IUpdateUserLogin } from "../models/command/Users/IUpdateUserLogin";
import { IUpdateUserPassword } from "../models/command/Users/IUpdateUserPassword";
import { IUserQueryResult } from "../models/result-request/Users/IUserQueryResult";
import AxiosBases from "./AxiosBases";

export class UserService {
    async getEvent(id: number, startPeriod: string, endPeriod: string): Promise<IEvent[]> {
        await CheckToken();

        return await AxiosBases.get<IEvent[]>("Api/Users/" + id + "/Event?startEvent=" + startPeriod + "&endEvent=" + endPeriod,
            { headers: { "Access-Token": (localStorage.getItem('access-token') + "").replaceAll("\"", "") } }).then((response) => {
                return response.data;
            }).catch(error => {
                return []
            })
    }

    async getById(id: number): Promise<IUserQueryResult> {
        await CheckToken();

        let response: IUserQueryResult = await AxiosBases.get<IUserQueryResult>("Api/Users/" + id,
            { headers: { "Access-Token": (localStorage.getItem('access-token') + "").replaceAll("\"", "") } })
            .then((response) => {
                return response.data;
            }).catch(error => {
                return error.response.data
            })
        return response;
    }

    async registrate(date: IRegistrateUser): Promise<IValidationResult> {
        let response: IValidationResult = await AxiosBases.post("Api/Users/Registrate/", date)
            .then((response) => {
                return response.data;
            }).catch(error => {
                return error.response.data.validationResult
            })
        return response;
    }

    async delete(id: number, date: IDeleteUser): Promise<IValidationResult> {
        await CheckToken();

        let response: IValidationResult = await AxiosBases.delete("Api/Users/" + id,
            { data: date, headers: { "Access-Token": (localStorage.getItem('access-token') + "").replaceAll("\"", "") } })
            .then((response) => {
                return response.data;
            }).catch(error => {
                return error.response.data.validationResult
            })
        return response;
    }

    async updateLogin(id: number, date: IUpdateUserLogin): Promise<IValidationResult> {
        await CheckToken();

        let response: IValidationResult = await AxiosBases.put("Api/Users/Update-Login/" + id, date,
            { headers: { "Access-Token": (localStorage.getItem('access-token') + "").replaceAll("\"", "") } })
            .then((response) => {
                return response.data;
            }).catch(error => {
                return error.response.data
            })
        return response;
    }

    async updatePassword(id: number, date: IUpdateUserPassword): Promise<IValidationResult> {
        await CheckToken();

        let response: IValidationResult = await AxiosBases.put("Api/Users/Update-Password/" + id, date,
            { headers: { "Access-Token": (localStorage.getItem('access-token') + "").replaceAll("\"", "") } })
            .then((response) => {
                return response.data;
            }).catch(error => {
                return error.response.data
            })
        return response;
    }
}