import axios from "axios";
import { StatusCode } from "../constant/constant";
import { toast } from "react-toastify";

const request = axios.create({
	//  baseURL: "https://web1.anasource.com/BookStore/api/BookStore/", // url = base url + request url
	// baseURL: 'https://localhost:44394/',
	// baseURL: "https://helperland1.azurewebsites.net/",
	baseURL:'https://localhost:7242/',
	timeout: 12400000,
	responseType: "json",
});

let requests: string[] = [];
let conflictRequest: string = "";

// Request interceptors Customize based on your need
request.interceptors.request.use(
	async (config: any) => {
		if (config.headers) {
			config.headers["Content-Type"] = "application/json";
		}

		if (config.headers["isDisableLoader"] !== true) {
			requests.push(config.url);
			showLoader();
		}

		return config;
	},
	(error: any) => {
		alert(error);
		Promise.reject(error);
	}
);

// Response interceptors Customize based on your need
request.interceptors.response.use(
	(response: any) => {
		const { data } = response;
		removeRequest(response.config.url);
		if (data?.code && data?.code !== "OK") {
			return Promise.reject(new Error(data.detail || "Error"));
		} else {
			return Promise.resolve(response);
		}
	},
	(error: any) => {
		removeRequest(error.config.url);
		toast.error(error.response.data.detail);
		return Promise.reject(error);
	}
);

function showLoader() {
	document.body.classList.add("loader-open");
}

function hideLoader() {
	document.body.classList.remove("loader-open");
}

// remove completed request
function removeRequest(req: string) {
	const i = requests.indexOf(req);
	if (i >= 0) {
		requests.splice(i, 1);
	}
	if (requests.length > 0) {
		showLoader();
	} else {
		hideLoader();
	}
	if (req === conflictRequest) {
		conflictRequest = "";
		requests = requests.filter((request) => request !== req);
	}
}

export default request;
