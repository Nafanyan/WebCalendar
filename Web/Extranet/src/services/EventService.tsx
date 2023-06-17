import{IEvent} from "../models/query/IEvent";
import AxiosDefault from "./AxiosDefault";

export class EventService{
    async get(id: number, startPeriod: string, endPeriod: string): Promise<IEvent>{
        return (await AxiosDefault.get<IEvent>("Users/"+ id +"/Events?startEvent=" + startPeriod + "&endEvent=" + endPeriod)).data;
    }
}