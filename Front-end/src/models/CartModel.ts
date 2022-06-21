import { BookModel } from "./BookModel";

export class CartModel {
 id?: number;
 userId!: number;
 bookId!: number;
 quantity!: number;
}

export class CartList {
 id!: number;
 userId!: number;
 book!: BookModel;
 quantity!: number;
}

export class GetCart {
 records!: CartList[];
 totalRecords!: number;
}
