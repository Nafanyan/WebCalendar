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

    let mounth: number = date.getMonth() + 1;
    result += (mounth < 10 ? "0" + mounth.toString() : mounth.toString()) + "-";

    let day: number = date.getDate();
    result += (day < 10 ? "0" + day.toString() : day.toString()) + "%";

    let hours: number = date.getHours();
    result += "20" + (hours < 10 ? "0" + hours.toString() : hours.toString()) + "%";

    let minute: number = date.getMinutes();
    result += "3A" + (minute < 10 ? "0" + minute.toString() : minute.toString());
    
    return result;
}
// StartEvent=2023-5-1%2000%3A00&endEvent=2023-5-31%2023%3A59 net::ER
// startEvent=2023-05-01%2000%3A00&endEvent=2023-05-31%2023%3A59