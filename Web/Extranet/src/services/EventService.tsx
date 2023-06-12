import{IEvent} from "../models/IEvent";
import AxiosDefault from "./AxiosDefault";

export class EventService{
    async get(): Promise<IEvent[]>{
        return (await  AxiosDefault.get<IEvent[]>("Users/4/")).data;
    }
}