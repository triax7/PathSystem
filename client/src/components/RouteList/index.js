import React from 'react'
import { observer } from 'mobx-react'
import Service from './service'
import { IconButton, Typography } from '@material-ui/core'
import Paper from '@material-ui/core/Paper'
import Grid from '@material-ui/core/Grid'
import Box from '@material-ui/core/Box'
import { Add, Done } from '@material-ui/icons'
import Button from '@material-ui/core/Button'
import { Field, Form, Formik } from 'formik'
import { TextField } from 'formik-material-ui'
import { history } from '../../stores/RouterStore'

const RouteList = observer(
  class extends React.Component {
    service

    constructor(props) {
      super(props)
      this.service = new Service()
    }

    render() {
      const {routes, isEditing, initialInput} = this.service.state
      const {handleEditing, handleSubmit, validateName} = this.service
      if (routes === undefined) return <Box>loading...</Box>
      return (
        <Box mt={6}>
          <Grid container justify={'center'}>
            <Grid item xs={6}>
              <Paper variant={'outlined'}>
                <Box display={'flex'} flexDirection={'column'} alignItems={'stretch'}>
                  <Box p={2} pb={0}>
                    {routes.length === 0 ?
                      <Box display={'flex'} justifyContent={'center'}>
                        <Typography>You have no routes.</Typography>
                      </Box> :
                      <Box border={4} borderColor={'#e9e9e9'} borderRadius={4} display={'flex'}
                           width={1} flexDirection={'column'} boxSizing={'border-box'}>
                        {routes.map(route => <Box key={route.id}>
                          <Paper square variant={'outlined'}>
                            <Box p={1} pl={2} display={'flex'} justifyContent={'space-between'}
                                 alignItems={'center'}>
                              <Typography>{route.name}</Typography>
                              <Button
                                onClick={() => history.push(`/routes/${route.id}`)}>Open</Button>
                            </Box>
                          </Paper>
                        </Box>)}
                      </Box>
                    }
                  </Box>
                  <Box display={'flex'} justifyContent={'center'} p={1}>
                    {!isEditing ?
                      <IconButton onClick={handleEditing}>
                        <Add/>
                      </IconButton> :
                      <Paper>
                        <Formik initialValues={{name: initialInput}} onSubmit={handleSubmit}>
                          <Form>
                            <Box p={1} display={'flex'} alignItems={'center'}
                                 justifyContent={'space-between'}>
                              <Field
                                component={TextField}
                                fullWidth
                                name={'name'}
                                placeholder={'Name'}
                                validate={validateName}
                              />
                              <IconButton type={'submit'}>
                                <Done/>
                              </IconButton>
                            </Box>
                          </Form>
                        </Formik>
                      </Paper>
                    }
                  </Box>
                </Box>
              </Paper>
            </Grid>
          </Grid>
        </Box>

      )
    }
  }
)

export default RouteList
