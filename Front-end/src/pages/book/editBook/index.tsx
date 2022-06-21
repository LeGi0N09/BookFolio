import React, { useEffect, useState } from "react";
import { editStyle } from "./style";
import * as Yup from "yup";
import {
  Typography,
  TextField,
  Button,
  Input,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
} from "@material-ui/core";
import { useHistory, useParams } from "react-router-dom";
import bookService from "../../../service/book.service";
import { Formik } from "formik";
import ValidationErrorMessage from "../../../components/ValidationErrorMessage/index";
import { toast } from "react-toastify";
import { materialCommonStyles } from "../../../utils/materialCommonStyles";
import categoryService from "../../../service/category.service";
import { BookModel } from "../../../models/BookModel";
import { CategoryModel } from "../../../models/CategoryModel";

const EditBook: React.FC = () => {
  const materialClasses = materialCommonStyles();
  const [categories, setCategories] = useState<CategoryModel[]>([]);
  const [selectedFile, setSelectedFile] = useState([]);
  const classes = editStyle();
  const history = useHistory();
  const initialValues: BookModel = {
    id: 0,
    name: "",
    price: "",
    categoryId: 0,
    description: "",
    base64image: "",
  };
  const [initialValueState, setInitialValueState] = useState<BookModel>(initialValues);
  const { id } = useParams<{ id?: string }>();

  useEffect(() => {
    if (id) getBookById();
    categoryService.getAll({pageIndex: 1}).then((res) => {
        setCategories(res.records);
    });
  }, [id]);

  const validationSchema = Yup.object().shape({
    name: Yup.string().required("Book Name is required"),
    description: Yup.string().required("Description is required"),
    categoryId: Yup.number()
      .min(1, "Category is required")
      .required("Category is required"),
    price: Yup.number().required("Price is required"),
    base64image: Yup.string().required("Image is required"),
  });

  const getBookById = (): void => {
    bookService.getById(Number(id)).then((res) => {
        setInitialValueState({
          id: res.id,
          name: res.name,
          price: res.price,
          categoryId: res.categoryId,
          description: res.description,
          base64image: res.base64image,
        });
    });
  };

  const onSubmit = (values: BookModel): void => {
    bookService.save(values).then((res) => {
      toast.success(values.id ? "Record updated successfully": "Record created successfully");
        history.push("/book");
    });
  };

  const onSelectFile = (e: any, setFieldValue: any, setFieldError: any): void => {
    const files = e.target.files;
    if (files?.length) {
      const fileSelected = e.target.files[0];
      const fileNameArray = fileSelected.name.split(".");
      const extension = fileNameArray.pop();
      if (["png", "jpg", "jpeg"].includes(extension?.toLowerCase())) {
        const reader = new FileReader();
        reader.readAsDataURL(fileSelected);
        reader.onload = function () {
          setFieldValue("base64image", reader.result);
        };
        reader.onerror = function (error) {
          throw error;
        };
      } else {
        toast.error("only jpg,jpeg and png files are allowed");
      }
    } else {
      setFieldValue("base64image", "");
    }
  };
  return (
    <div className={classes.editWrapper}>
      <div className="container">
        <Typography variant="h1">{id ? "Edit" : "Add"} Book</Typography>
        <Formik
          initialValues={initialValueState}
          validationSchema={validationSchema}
          enableReinitialize={true}
          onSubmit={onSubmit}
        >
          {({
            values,
            errors,
            touched,
            handleBlur,
            handleChange,
            handleSubmit,
            setValues,
            setFieldError,
            setFieldValue,
          }) => (
            <form onSubmit={handleSubmit}>
              <div className="form-row-wrapper">
                <div className="form-col">
                  <TextField
                    id="first-name"
                    name="name"
                    label="Book Name *"
                    variant="outlined"
                    inputProps={{ className: "small" }}
                    value={values.name}
                    onBlur={handleBlur}
                    onChange={handleChange}
                  />
                  <ValidationErrorMessage
                    message={errors.name}
                    touched={touched.name}
                  />
                </div>

                <div className="form-col">
                  <TextField
                    type={"number"}
                    id="price"
                    name="price"
                    label="Book Price (RS)*"
                    variant="outlined"
                    inputProps={{ className: "small" }}
                    value={values.price}
                    onBlur={handleBlur}
                    onChange={handleChange}
                  />
                  <ValidationErrorMessage
                    message={errors.price}
                    touched={touched.price}
                  />
                </div>

                <div className="form-col">
                  <FormControl className="dropdown-wrapper" variant="outlined">
                    <InputLabel htmlFor="select">Category *</InputLabel>
                    <Select
                      name={"categoryId"}
                      id={"category"}
                      onChange={handleChange}
                      className={materialClasses.customSelect}
                      MenuProps={{
                        classes: { paper: materialClasses.customSelect },
                      }}
                      value={values.categoryId}
                    >
                      {categories?.map((rl: any) => (
                        <MenuItem value={rl.id} key={"category" + rl.id}>
                          {rl.name}
                        </MenuItem>
                      ))}
                    </Select>
                  </FormControl>
                  <ValidationErrorMessage
                    message={errors.categoryId}
                    touched={touched.categoryId}
                  />
                </div>
                {/* <img src={values.imageSrc} alt="asa" /> */}
                <div className="form-col">
                  {!values.base64image && (
                    <>
                      {" "}
                      <label
                        htmlFor="contained-button-file"
                        className="file-upload-btn"
                      >
                        <Input
                          id="contained-button-file"
                          type="file"
                          inputProps={{ className: "small" }}
                          onBlur={handleBlur}
                          onChange={(e) => {
                            onSelectFile(e, setFieldValue, setFieldError);
                          }}
                        />
                        <Button
                          variant="contained"
                          component="span"
                          className="btn pink-btn"
                        >
                          Upload
                        </Button>
                      </label>
                      <ValidationErrorMessage
                        message={errors.base64image}
                        touched={touched.base64image}
                      />
                    </>
                  )}
                  {values.base64image && (
                    <div className="uploaded-file-name">
                      <em>
                        <img src={values.base64image} alt="" />
                      </em>
                      image{" "}
                      <span
                        onClick={() => {
                          setFieldValue("base64image", "");
                        }}
                      >
                        x
                      </span>
                    </div>
                  )}
                </div>
                <div className="form-col full-width description">
                  <TextField
                    id="description"
                    name="description"
                    label="Description *"
                    variant="outlined"
                    value={values.description}
                    multiline
                    onBlur={handleBlur}
                    onChange={handleChange}
                  />
                  <ValidationErrorMessage
                    message={errors.description}
                    touched={touched.description}
                  />
                </div>
              </div>
              <div className="btn-wrapper">
                <Button
                  className="green-btn btn"
                  variant="contained"
                  type="submit"
                  color="primary"
                  disableElevation
                >
                  Save
                </Button>
                <Button
                  className="pink-btn btn"
                  variant="contained"
                  type="button"
                  color="primary"
                  disableElevation
                  onClick={() => {
                    history.push("/book");
                  }}
                >
                  Cancel
                </Button>
              </div>
            </form>
          )}
        </Formik>
      </div>
    </div>
  );
};

export default EditBook;
