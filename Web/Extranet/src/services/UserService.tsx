import { IValidationResult } from "../models/command/IValidationResult";
import { IEvent } from "../models/query/IEvent";
import AxiosDefault from "./AxiosDefault";
import { IAddEvent } from "../models/command/IAddEvent";
import { AxiosError } from "axios";

export class UserService{
    async getEvent(id: number, startPeriod: string, endPeriod: string): Promise<IEvent[]>{
        return (await AxiosDefault.get<IEvent[]>("Users/"+ id +"/Event?startEvent=" + startPeriod + "&endEvent=" + endPeriod)).data;
    }

    async addEvent(id: number, date: IAddEvent) : Promise<IValidationResult>{
        console.log(date);
        let validationResult: IValidationResult = (await AxiosDefault.post("Users/"+ id +"/Events", date)).data;
        if (AxiosError)
        {
            console.log(validationResult);
        }
        
        return validationResult;
    }
}