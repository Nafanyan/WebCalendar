export function TimeToString(date: Date): string {
    let nowDate: Date = new Date(date.toString());
    let result: string = "";

    let hours: number = nowDate.getHours();
    result += (hours < 10 ? "0" + hours.toString() : hours.toString()) + ":";

    let minutes: number = nowDate.getMinutes();
    result += minutes < 10 ? "0" + minutes.toString() : minutes.toString();
    return result;
}

export function TimeToStringRequest(date: Date): string {
    let result: string = "";
    result += date.getFullYear().toString() + "-";
    result += (date.getMonth() + 1).toString() + "-";
    result += date.getDate().toString() + " ";
    let hours: number = date.getHours();
    result += (hours < 10 ? "0" + hours.toString() : hours.toString()) + ":";
    let minute: number = date.getMinutes();
    result += minute < 10 ? "0" + minute.toString() : minute.toString();
    return result;
}

export function TimeToStringCommand(date: Date, time: string): string {
    let result: string = "";
    result += date.getFullYear().toString() + "-";
    let month: number = date.getMonth() + 1;
    result += (month < 10 ? "0" + month.toString() : month.toString()) + "-";
    let day: number = date.getDate();
    result += (day < 10 ? "0" + day.toString() : day.toString()) + "T";
    result += time;
    return result;
}
