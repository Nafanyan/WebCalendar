import { IEvent } from "../models/IEvent";
import AxiosDefault from "./AxiosDefault";

export class UserService{
    async getEvent(id: number, startPeriod: string, endPeriod: string): Promise<IEvent[]>{
        return (await AxiosDefault.get<IEvent[]>("api/Users/"+ id +"/Event?startEvent=" + startPeriod + "&endEvent=" + endPeriod)).data;
    }
}