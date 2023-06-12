
export class SettingDateForUser{
    userId: number;
    year: number;
    month: number;
    day: number;
    months: Array<string>;
    constructor() {
        this.userId = 4;
        this.year = 2023;
        // new Date().getFullYear();
        this.month = 5;
        // new Date().getMonth();
        this.day = 31; 
        // new Date().getDate();
        this.months = ['','Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'];
      }
}