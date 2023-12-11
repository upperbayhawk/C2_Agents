-- Copyright 2021 SmartThings
--
-- Licensed under the Apache License, Version 2.0 (the "License");
-- you may not use this file except in compliance with the License.
-- You may obtain a copy of the License at
--
--     http://www.apache.org/licenses/LICENSE-2.0
--
-- Unless required by applicable law or agreed to in writing, software
-- distributed under the License is distributed on an "AS IS" BASIS,
-- WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
-- See the License for the specific language governing permissions and
-- limitations under the License.

local DIMMER = {
  PARAMETERS = {
    motorAfterPower     = { type = 'config', parameter_number = 20, size = 1 },
	shutterDirection     = { type = 'config', parameter_number = 22, size = 1 },
	bladeTurnTime     = { type = 'config', parameter_number = 34, size = 2 },
	shutterTime     = { type = 'config', parameter_number = 35, size = 2 },
	curtainMode     = { type = 'config', parameter_number = 39, size = 1 },
    autoReportType		= {  type = 'config', parameter_number = 80, size = 1  },
	externalButtonOp		= {  type = 'config', parameter_number = 85, size = 1  },
	s1External    = { type = 'config', parameter_number = 120, size = 1 },
	s2External    = { type = 'config', parameter_number = 121, size = 1 },
	calibrateShutter    = { type = 'config', parameter_number = 36, size = 1 },
	calibrateShutterNext    = { type = 'config', parameter_number = 37, size = 1 },
	repositionShutter    = { type = 'config', parameter_number = 40, size = 1 },
	calibrationLock    = { type = 'config', parameter_number = 42, size = 1 },
    assocGroup1          = { type = 'assoc', group = 1, maxnodes = 5, addhub = false }
  }
}


local devices = {
  AEOTEC_NANO_SHUTTER = {
    MATCHING_MATRIX = {
      mfrs = 0x0371,
      product_ids = 0x008D
    },
    PARAMETERS = DIMMER.PARAMETERS
  }
}

local preferences = {}

preferences.get_device_parameters = function(zw_device)
  for _, device in pairs(devices) do
    if zw_device:id_match(
      device.MATCHING_MATRIX.mfrs,
      device.MATCHING_MATRIX.product_types,
      device.MATCHING_MATRIX.product_ids) then
      return device.PARAMETERS
    end
  end
  return nil
end

preferences.to_numeric_value = function(new_value)
  local numeric = tonumber(new_value)
  if numeric == nil then -- in case the value is boolean
    numeric = new_value and 1 or 0
  end
  return numeric
end

return preferences