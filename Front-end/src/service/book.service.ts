import request from "./request";
import FilterModel from "../models/FilterModel";
import { BookModel } from "../models/BookModel";
import BaseList from "../models/BaseList";

class BookService {
	ENDPOINT = "api/book";

	public async getAll(params: FilterModel): Promise<BaseList<BookModel[]>> {
		const url = `${this.ENDPOINT}/list`;
		return request.get<BaseList<BookModel[]>>(url, { params }).then((res) => {
			return res.data;
		});
	}

	public async getById(id: number): Promise<BookModel> {
		const url = `${this.ENDPOINT}/{id}`;
		return request.get<BookModel>(url).then((res) => {
			return res.data;
		});
	}

	public async delete(id: number): Promise<BookModel> {
		const url = `${this.ENDPOINT}/delete/${id}`;
		return request.delete<BookModel>(url).then((res) => {
			return res.data;
		});
	}

	public async save(data: BookModel): Promise<any> {
		data.publisherId = 1;
		data.quantity = 10;
		if (data.id) {
			const url = `${this.ENDPOINT}/update`;
			return request.put<BookModel>(url, data).then((res) => {
				return res.data;
			});
		} else {
			const url = `${this.ENDPOINT}/add`;
			return request.post<BookModel>(url, data).then((res) => {
				return res.data;
			});
		}
	}
}
export default new BookService();
