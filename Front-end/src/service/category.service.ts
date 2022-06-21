import request from "./request";
import FilterModel from "../models/FilterModel";
import BaseList from "../models/BaseList";
import { CategoryModel } from "../models/CategoryModel";

class CategoryService {
    ENDPOINT = 'api/category';

    public async getAll(params: FilterModel): Promise<BaseList<CategoryModel[]>> {
        const url = `${this.ENDPOINT}/list`;
        return request.get<BaseList<CategoryModel[]>>(url, {params} ).then((res) => {
            return res.data;
        });
    }

    public async getById(id: number): Promise<CategoryModel> {
        const url = `${this.ENDPOINT}/{id}`;
        return request.get<CategoryModel>(url).then((res) => {
            return res.data;
        });
    }

    public async delete(id: number): Promise<CategoryModel> {
        const url = `${this.ENDPOINT}/delete/{id}`;
        return request.delete<CategoryModel>(url).then((res) => {
            return res.data;
        });
    }

    public async save(data: CategoryModel): Promise<CategoryModel> {
        if (data.id) {
            const url = `${this.ENDPOINT}/update`;
            return request.put<CategoryModel>(url, data ).then((res) => {
                return res.data;
            });
        } else {
            const url = `${this.ENDPOINT}/add`;
            return request.post<CategoryModel>(url, data ).then((res) => {
                return res.data;
            });
        }
    }

}
export default new CategoryService();
