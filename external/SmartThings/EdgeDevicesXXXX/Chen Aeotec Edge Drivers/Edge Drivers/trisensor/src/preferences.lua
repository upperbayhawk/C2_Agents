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

local SWITCH = {
  PARAMETERS = {
    motionRetrigger          = { type = 'config', parameter_number = 1, size = 2 },
	motionTimeout          = { type = 'config', parameter_number = 2, size = 2 },
	motionSensitivity          = { type = 'config', parameter_number = 3, size = 1 },
	ledEnable          = { type = 'config', parameter_number = 10, size = 1 },
	ledMotion          = { type = 'config', parameter_number = 11, size = 1 },
	ledTemp          = { type = 'config', parameter_number = 12, size = 1 },
	ledLux          = { type = 'config', parameter_number = 13, size = 1 },
	ledBattery          = { type = 'config', parameter_number = 14, size = 1 },
	ledWakeUp          = { type = 'config', parameter_number = 15, size = 1 },
	threshTemp          = { type = 'config', parameter_number = 21, size = 2 },
	threshLight          = { type = 'config', parameter_number = 22, size = 2 },
	intervalTemp          = { type = 'config', parameter_number = 23, size = 2 },
	intervalLight          = { type = 'config', parameter_number = 24, size = 2 },
	offsetTemp          = { type = 'config', parameter_number = 30, size = 2 },
	offsetLight          = { type = 'config', parameter_number = 31, size = 2 },
	assocGroup1          = { type = 'assoc', group = 1, maxnodes = 5, addhub = true },
	assocGroup2          = { type = 'assoc', group = 2, maxnodes = 5, addhub = false },
	assocGroup3          = { type = 'assoc', group = 2, maxnodes = 5, addhub = false }
  }
}


local devices = {
  AEOTEC_TRISENSOR_EU = {
    MATCHING_MATRIX = {
      mfrs = 0x0371,
	  product_types = 0x0002,
      product_ids = 0x0005
    },
    PARAMETERS = SWITCH.PARAMETERS
  },
  AEOTEC_TRISENSOR_US = {
    MATCHING_MATRIX = {
      mfrs = 0x0371,
	  product_types = 0x0102,
      product_ids = 0x0005
    },
    PARAMETERS = SWITCH.PARAMETERS
  },
  AEOTEC_TRISENSOR_AU = {
    MATCHING_MATRIX = {
      mfrs = 0x0371,
	  product_types = 0x0202,
      product_ids = 0x0005
    },
    PARAMETERS = SWITCH.PARAMETERS
  },
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