
export class SettingDateForUser{
    userId: number;
    year: number;
    month: number;
    day: number;
    
    constructor() {
        this.userId = 4;
        this.year = new Date().getFullYear();
        this.month = new Date().getMonth() + 1;
        this.day = new Date().getDate();
      }
}