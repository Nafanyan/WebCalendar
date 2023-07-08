import axios from "axios";

export default axios.create({
    // baseURL: "http://194.58.109.241:5000",
    headers: {
        "Accept": "application/json",
        "Content-Type": "application/json"
    },
});