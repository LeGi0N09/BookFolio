export class OrderAddModel {
	id?: number;
	userId!: number;
	orderDate?: string;
	cartIds!: number[]
}

export class SubOrderModel {
	bookId!: number;
	quantity!: number;
	price!: number;
	totalPrice!: number;
}
