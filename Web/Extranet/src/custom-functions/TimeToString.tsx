export function TimeToString(date: Date): string {
    let nowDate: Date = new Date(date.toString());
    let result: string = "";
    result += nowDate.getHours().toString() + ":";
    result += nowDate.getMinutes() < 10 ? "0" + nowDate.getMinutes().toString() : nowDate.getMinutes().toString();
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
