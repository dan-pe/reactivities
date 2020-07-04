import React, { useState, useContext, useEffect } from "react";
import { Segment, Form, Button, Grid } from "semantic-ui-react";
import { IActivity } from "../../../app/models/activity";
import ActivityStore from "../../../app/stores/activityStore";
import { observer } from "mobx-react-lite";
import { RouteComponentProps } from "react-router-dom";
import { Form as FinalForm, Field } from "react-final-form";
import TextInput from "../../../app/common/form/TextInput";
import TextAreaInput from "../../../app/common/form/TextAreaInput";
import SelectInput from "../../../app/common/form/SelectInput";
import { category } from "../../../app/common/options/categoryOptions";

interface DetailParams {
  id: string;
}

const ActivityForm: React.FC<RouteComponentProps<DetailParams>> = ({
  match,
  history,
}) => {
  const activityStore = useContext(ActivityStore);
  const {
    submitting,
    activity: initialFormState,
    loadActivity,
    clearActivity,
  } = activityStore;

  const [activity, setActivity] = useState<IActivity>({
    id: "",
    title: "",
    category: "",
    description: "",
    date: "",
    city: "",
    venue: "",
  });

  useEffect(() => {
    if (match.params.id && activity.id.length === 0) {
      loadActivity(match.params.id).then(
        () => initialFormState && setActivity(initialFormState)
      );
    }
    return () => {
      clearActivity();
    };
  }, [
    loadActivity,
    clearActivity,
    match.params.id,
    initialFormState,
    activity.id.length,
  ]);

  // const handleSubmit = () => {
  //   if (activity.id.length === 0) {
  //     let newActivity = {
  //       ...activity,
  //       id: uuid(),
  //     };
  //     createActivity(newActivity).then(() => {
  //       history.push(`activities/${newActivity.id}`);
  //     });
  //   } else {
  //     editActivity(activity).then(() => {
  //       history.push(`activities/${activity.id}`);
  //     });
  //   }
  // };

  const handleFinalFormSubmit = (values: any) => {
    console.log(values);
  };

  return (
    <Grid>
      <Grid.Column width={10}>
        <Segment clearing>
          <FinalForm
            onSubmit={handleFinalFormSubmit}
            render={({ handleSubmit }) => (
              <Form onSubmit={handleSubmit}>
                <Field
                  placeholder="Title"
                  name="title"
                  value={activity.title}
                  component={TextInput}
                />
                <Field
                  placeholder="Description"
                  name="description"
                  rows={3}
                  value={activity.description}
                  component={TextAreaInput}
                />
                <Field
                  placeholder="Category"
                  name="category"
                  component={SelectInput}
                  options={category}
                  value={activity.category}
                />
                <Field
                  component={TextInput}
                  placeholder="Date"
                  name="date"
                  value={activity.date}
                />
                <Field
                  placeholder="City"
                  name="city"
                  component={TextInput}
                  value={activity.city}
                />
                <Field
                  placeholder="Venue"
                  name="venue"
                  component={TextInput}
                  value={activity.venue}
                />
                <Button
                  loading={submitting}
                  floated="right"
                  positive
                  type="submit"
                  content="Submit"
                />
                <Button
                  onClick={() => history.push("/activities")}
                  floated="right"
                  type="button"
                  content="Cancel"
                />
              </Form>
            )}
          />
        </Segment>
      </Grid.Column>
    </Grid>
  );
};

export default observer(ActivityForm);
