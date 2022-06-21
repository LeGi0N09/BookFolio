import { OrderAddModel } from "../models/OrderModel";
import request from "./request";

class OrderService {
	ENDPOINT = "api/order";

	public async placeOrder(order: OrderAddModel): Promise<OrderAddModel> {
		const url = `${this.ENDPOINT}/add`;
		return request
			.post<OrderAddModel>(url, order)
			.then((res) => {
				return res.data;
			})
			.catch((e) => {
				return Promise.reject(e);
			});
	}
}

export default new OrderService();
