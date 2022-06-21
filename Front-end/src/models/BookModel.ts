export class BookModel {
	id?: number;
	name!: string;
	price!: string;
	categoryId!: number;
	category?: string;
	description!: string;
	base64image!: string;
	publisherId?: number;
	quantity?: number;
}
