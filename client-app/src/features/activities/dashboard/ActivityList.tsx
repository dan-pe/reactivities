import React, {  useContext } from "react";
import { Item, Label, Button, Segment } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import ActivityStore from "../../../app/stores/activityStore";

const ActivityList: React.FC = () => {
  const activityStore = useContext(ActivityStore);
  const { activitiesByDate, selectActivity, deleteActivity, submitting, target } = activityStore;

  return (
    <Segment clearing>
      <Item.Group divided>
        {activitiesByDate.map(activitiy => (
          <Item key={activitiy.id}>
            <Item.Content>
              <Item.Header as="a">{activitiy.title}</Item.Header>
              <Item.Meta>{activitiy.date}</Item.Meta>
              <Item.Description>
                <div>{activitiy.description}</div>
                <div>
                  {activitiy.city}, {activitiy.venue}
                </div>
              </Item.Description>
              <Item.Extra>
                <Button
                  onClick={() => selectActivity(activitiy.id)}
                  floated="right"
                  content="View"
                  color="blue"
                />
                <Button
                  name={activitiy.id}
                  loading={target === activitiy.id && submitting}
                  onClick={e => deleteActivity(e, activitiy.id)}
                  floated="right"
                  content="Delete"
                  color="red"
                />
                <Label basic content={activitiy.category} />
              </Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </Segment>
  );
};

export default observer(ActivityList);
