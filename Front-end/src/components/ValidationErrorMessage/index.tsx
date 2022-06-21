import React from "react";

type IProps = {
  message?: string;
  touched?: boolean;
};

const ValidationErrorMessage: React.FC<IProps> = (props) => {
  return (
    <>{props.touched && <p className=" mb-1 text-danger">{props.message}</p>}</>
  );
};

export default ValidationErrorMessage;
